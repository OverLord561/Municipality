using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Municipality.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplicationBuilderUseSwagger(this IApplicationBuilder builder)
        {

            builder.UseSwagger();

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Municipality API V1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
