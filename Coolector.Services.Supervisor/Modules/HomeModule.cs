using Coolector.Services.Supervisor.Settings;

namespace Coolector.Services.Supervisor.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule(SupervisorSettings settings) : base(requireAuthentication: false)
        {
            Get("", args => "Welcome to the Coolector.Services.Supervisor API!");
            
            Get("dashboard", args => View["wwwroot/views/dashboard/index", settings]);
        }
    }
}