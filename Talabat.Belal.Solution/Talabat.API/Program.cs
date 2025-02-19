
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using Talabat.API.Errors;

using Talabat.API.Extensions;
using Talabat.API.Helper;
using Talabat.API.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

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


            // clean up program use extension method
            webApplicationBuilder.Services.AddSwaggerServices();


            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            webApplicationBuilder.Services.AddDbContext<AppIdentityDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {

                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection!);
            });


            // clean up program use extension method
            webApplicationBuilder.Services.AddApplicationServices();



            // clean up program use extension method
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            //webApplicationBuilder.Services.AddScoped(typeof(IAuthService), typeof(AuthServices));

            //// add Identity Services configuration (UserManager , SigninManager , RoleManager)
            //webApplicationBuilder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            //{
            //    //options.Password.RequiredUniqueChars = 2;
            //    //options.Password.RequireNonAlphanumeric = true;
            //    //options.Password.RequireUppercase = true;
            //    //options.Password.RequireLowercase = true;

            //}).AddEntityFrameworkStores<AppIdentityDbContext>();

            #endregion


            var app = webApplicationBuilder.Build();



            #region Ask CLR for creating Object from DbContext Explicitly
            // ask CLR for creating Object from DbContext Explicitly

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();

            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {

                await _dbContext.Database.MigrateAsync(); // for automatically update database

                await StoreContextSeed.SeedAsync(_dbContext); // for seeding entered data

                await _identityDbContext.Database.MigrateAsync(); // for automatically update database

                var _userManager = services.GetRequiredService<UserManager<AppUser>>(); // Explicitly
                await AppIdentityDbContextSeed.identitySeedAsync(_userManager);
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
                // clean up program use extension method
                app.UseSwaggerMiddleware();
            }


            //app.UseStatusCodePagesWithRedirects("/Errors/{0}");
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.MapControllers();


            app.UseAuthentication();
            app.UseAuthorization();

            #endregion


            app.Run();
        }
    }
}
