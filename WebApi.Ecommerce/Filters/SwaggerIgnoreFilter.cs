using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace WebApi.Ecommerce.Filters
{
    public class SwaggerIgnoreFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //var ignoredProperties = context.MethodInfo.GetParameters()
            //                            .SelectMany(p => p.ParameterType.GetProperties()
            //                            .Where(prop => prop.GetCustomAttribute<SwaggerIgnoreAttribute>() != null))
            //                            .ToList();

            var ignoredPropertiesCustom = context.MethodInfo.GetParameters()
                                            .SelectMany(p => p.ParameterType.GetProperties())
                                            .ToList();

            var excludeProperties = new[] { "Notifications", "Invalid", "Valid", "IsValid" };

            if (ignoredPropertiesCustom.Any())
            {
                foreach (var property in ignoredPropertiesCustom)
                {
                    operation.Parameters = operation.Parameters
                        .Where(p => (!excludeProperties.Contains(p.Name)))
                        .ToList();
                }
            }

            //if (ignoredProperties.Any())
            //{
            //    foreach (var property in ignoredProperties)
            //    {
            //        operation.Parameters = operation.Parameters
            //            .Where(p => (!p.Name.Equals(property.Name, StringComparison.InvariantCulture) && !excludeProperties.Contains(p.Name)))
            //            .ToList();
            //    }
            //}
        }
    }
}
