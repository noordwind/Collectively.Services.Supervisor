using Collectively.Common.Host;
using Collectively.Services.Supervisor.Framework;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Collectively.Services.Supervisor
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     // WebServiceHost
        //     //     .Create<Startup>(args: args)
        //     //     .Build()
        //     //     .Run();
        // }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}