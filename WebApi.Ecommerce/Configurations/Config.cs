using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApi.Ecommerce.Domain.Providers;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Infra.Repositories;
using WebApi.Ecommerce.Services.Providers;
using WebApi.Ecommerce.Services.Services;

namespace WebApi.Ecommerce.Configurations
{
    public static class Config
    {
        public static IServiceCollection DependencyResolver(this IServiceCollection services, IConfiguration configuration)
        {
            // Set type scope
            SetScoped(services);

            // Set default setting
            SetSettings(configuration);

            return services;
        }

        private static void SetScoped(IServiceCollection services)
        {
            #region Singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Scoped

            #region Repository

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILogErroRepository, LogErroRepository>();
            services.AddScoped<ILogRequestRepository, LogRequestRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleProductRepository, SaleProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleTypeRepository, SaleTypeRepository>();

            #endregion

            #region Service

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleTypeService, SaleTypeService>();

            #endregion

            #region Provider

            services.AddScoped<IRequestProvider, RequestProvider>();
            services.AddScoped<IZipCodeProvider, ZipCodeProvider>();

            #endregion

            #endregion
        }

        private static void SetSettings(IConfiguration configuration)
        {
            // Settings
            Settings.ViaCep = configuration.GetSection("HelpUrl").GetSection("ViaCep").Value;
            Settings.City = configuration.GetSection("Shipping").GetSection("City").Value;
            Settings.State = configuration.GetSection("Shipping").GetSection("State").Value;
            Settings.StepOne = Convert.ToDecimal(configuration.GetSection("Shipping").GetSection("StepOne").Value);
            Settings.StepTwo = Convert.ToDecimal(configuration.GetSection("Shipping").GetSection("StepTwo").Value);
            Settings.StepThree = Convert.ToDecimal(configuration.GetSection("Shipping").GetSection("StepThree").Value);
        }
    }
}
