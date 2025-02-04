using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services )
        {
            #region allow_dependancyInjection_for_repository
            //services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();
            //services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>(); 

            // generic 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ProductPictureUrlResolver>();

            #endregion


            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            //services.AddAutoMapper(typeof(MappingProfiles));






            services.Configure<ApiBehaviorOptions>(options =>
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


            return services;
        }
    }
}




/*
 
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
 
 */
