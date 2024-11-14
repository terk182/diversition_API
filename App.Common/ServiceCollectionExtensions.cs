using App.Auth.Services;
using App.Products.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<JwtTokenService>();
            services.AddSingleton<UserService>();

            return services;
        }
    }
}
