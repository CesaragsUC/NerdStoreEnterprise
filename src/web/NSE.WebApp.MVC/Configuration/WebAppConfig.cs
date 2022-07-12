using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.WebApp.MVC.Extentions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {

        public static void AddMvcConfiguration(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddControllersWithViews();


            //config do MVC pra trabalhar com Proxy NGnix
            service.Configure<ForwardedHeadersOptions>(
                options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                });

            service.Configure<AppSettings>(configuration);
        }


        public static void UseMvcConfiguration(this IApplicationBuilder app,IWebHostEnvironment env)
        {

            //precizsa ser o primeiro a ser chamado
            app.UseForwardedHeaders();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();

            //}
            //else
            //{
            //    app.UseExceptionHandler("/erro/500");
            //    app.UseStatusCodePagesWithRedirects("/erro/{0}");
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //config do identity sempre deve estar entre routing e endpoints
            app.UseItentityConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalogo}/{action=Index}/{id?}");
            });

        }
    }
}
