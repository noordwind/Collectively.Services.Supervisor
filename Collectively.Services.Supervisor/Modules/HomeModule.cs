using Collectively.Services.Supervisor.Settings;

namespace Collectively.Services.Supervisor.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule(SupervisorSettings settings) : base(requireAuthentication: false)
        {
            Get("", args => "Welcome to the Collectively.Services.Supervisor API!");
            
            Get("dashboard", args => View["wwwroot/views/dashboard/index", settings]);
        }
    }
}