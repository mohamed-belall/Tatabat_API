
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.API.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            //StoreContext updatedatabase = new StoreContext();
            //updatedatabase.Database.MigrateAsync();

            var webApplicationBuilder = WebApplication.CreateBuilder(args);


            #region Configure_Service

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();


            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });



            #region allow_dependancyInjection_for_repository
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>(); 

            // generic 
            webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));

            webApplicationBuilder.Services.AddScoped<ProductPictureUrlResolver>();

            #endregion


            webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            //webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));






            webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                // factory المسوال عن ال response بتاع ال end point اللي بتتنفذ حاليا 
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // modelState => is a dictionary or each value pair for each parameter
                    // key => parameter name
                    // value => list of errors

                    var errors = actionContext
                    .ModelState
                    .Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);

                };
            });
            #endregion


            var app = webApplicationBuilder.Build();


            #region Ask CLR for creating Object from DbContext Explicitly
            // ask CLR for creating Object from DbContext Explicitly

            using var scope = app.Services.CreateScope();
          
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {

                await _dbContext.Database.MigrateAsync(); // for automatically update database

                await StoreContextSeed.SeedAsync(_dbContext); // for seeding entered data
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occurred during apply the Migration");
                
            }



            // this is another way to dispose scope or using ( using ) 
            //var scope = app.Services.CreateScope();
            //try
            //{

            //    var services = scope.ServiceProvider;
            //    var _dbContext = services.GetRequiredService<DbContext>();

            //    await _dbContext.Database.MigrateAsync();

            //}
            //finally { scope.Dispose(); }

            #endregion


            #region Configure Kestrel MiddleWare

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //app.UseStatusCodePagesWithRedirects("/Errors/{0}");
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            #endregion


            app.Run();
        }
    }
}
