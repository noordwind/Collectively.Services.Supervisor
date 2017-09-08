using Lockbox.Client.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Collectively.Services.Supervisor.Framework;
using Collectively.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Collectively.Services.Supervisor
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IServiceCollection Services { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .SetBasePath(env.ContentRootPath);

            if (env.IsProduction() || env.IsDevelopment())
            {
                builder.AddLockbox();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSerilog(Configuration);
            // services.AddWebEncoders();
            // services.AddCors();
            Services = services;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseSerilog(loggerFactory);
            // app.UseCors(builder => builder.AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowAnyOrigin()
            //    .AllowCredentials());
            app.UseOwin().UseNancy(x => x.Bootstrapper = new Bootstrapper(Configuration, Services));
        }
    }
}