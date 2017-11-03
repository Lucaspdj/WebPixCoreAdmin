using Microsoft.AspNetCore.Builder;

namespace WebPixCoreAdmin.Helpers.Ext
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomHanlderMiddleware
                                     (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
        public static IApplicationBuilder UseCustomModuleMiddleware
                                  (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
