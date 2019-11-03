using System;
using AutoMapper;
using Codecamp.Mobile.Clients.Portable.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shiny;
using Shiny.Jobs;
using Xamarin.Essentials;

namespace Codecamp.Mobile.Clients.Portable
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    //c.AddJsonFile(configLocation);
                })
                .ConfigureServices((c, x) =>
                {
                    nativeConfigureServices(c, x);
                    ConfigureServices(c, x);
                })
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
                .Build();

            ServiceProvider = host.Services;
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {

            services.AddGlobalServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            if (ctx.HostingEnvironment.IsDevelopment())
            {
                
            }
            else
            {
                
            }
            services.AddViewModels();

        }
    }
}
