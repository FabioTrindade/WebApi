using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Filters;
using WebApi.Ecommerce.Infra.Contexts;
using WebApi.Ecommerce.Middlewares;

namespace WebApi.Ecommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            // Add route lower case
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            var host = Configuration["DBHOST"] ?? "localhost";
            var password = Configuration["DBPASSWORD"] ?? "p0stGr3s";

            // Document swagger
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerIgnoreFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Ecommerce", Version = "v1" });
            });

            // Connection with database
            services.AddDbContext<WebApiDataContext>(options =>
                options
                    .UseNpgsql(string.Format(Configuration.GetConnectionString("WebApiConnection"), host, password), m => m.MigrationsHistoryTable("WebApiEcommerceMigrations"))
                    .UseLowerCaseNamingConvention()
            );

            // Dependecy Injection
            services.DependencyResolver(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Initialize database
            InitializeDatabase(app);

            SetConfigureSwagger(app);

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            // Middleware Logging Request and Response
            app.UseMiddleware<RequestAndResponseLoggingMiddleware>();

            // Middleware Erro
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SetConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Ecommerce v1");
                c.RoutePrefix = "swagger";
                c.DefaultModelsExpandDepth(-1);
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }

        /// Create database end execute migrations on startar project
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<WebApiDataContext>().Database.Migrate();
        }
    }
}
