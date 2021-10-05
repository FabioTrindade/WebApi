using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            SetScoped(services, configuration);

            return services;
        }

        private static void SetScoped(IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddScoped<IProductService, ProductService>();

            #endregion

            #region Provider

            services.AddScoped<IRequestProvider, RequestProvider>();

            #endregion

            #endregion
        }
    }
}
