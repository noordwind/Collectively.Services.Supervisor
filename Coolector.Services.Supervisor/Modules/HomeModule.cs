namespace Coolector.Services.Supervisor.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule() : base(requireAuthentication: false)
        {
            Get("", args => "Welcome to the Coolector.Services.Supervisor API!");
            
            Get("dashboard", args => View["wwwroot/views/dashboard/index.html"]);
        }
    }
}