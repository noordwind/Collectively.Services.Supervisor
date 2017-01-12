using Coolector.Common.Host;
using Coolector.Services.Supervisor.Framework;

namespace Coolector.Services.Supervisor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 11000)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .Build()
                .Run();
        }
    }
}