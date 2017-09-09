using Collectively.Common.Host;
using Collectively.Services.Supervisor.Framework;

namespace Collectively.Services.Supervisor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(args: args)
                .Build()
                .Run();
        }
    }
}