using Collectively.Services.Supervisor.Domain;
using Collectively.Services.Supervisor.Queries;
using Collectively.Services.Supervisor.Services;

namespace Collectively.Services.Supervisor.Modules
{
    public class SupervisorModule : ModuleBase
    {
        public SupervisorModule(ISupervisorService supervisorService)
        {
            Get("supervisor", args => Fetch<GetSupervisorResult, SupervisorResult>
                (async x => await supervisorService.CheckServicesAsync()).HandleAsync());
        }
    }
}