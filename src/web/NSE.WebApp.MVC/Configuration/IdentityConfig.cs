using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NSE.WebApp.MVC.Configuration
{
    public static class IdentityConfig
    {
        public static void AddItentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(optons =>
                {
                    optons.LoginPath = "/login";
                    optons.AccessDeniedPath = "/acesso-negado";
                });
        }
        public static void UseItentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
