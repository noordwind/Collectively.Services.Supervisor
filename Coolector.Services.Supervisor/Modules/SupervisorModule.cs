using Coolector.Services.Supervisor.Domain;
using Coolector.Services.Supervisor.Queries;
using Coolector.Services.Supervisor.Services;

namespace Coolector.Services.Supervisor.Modules
{
    public class SupervisorModule : ModuleBase
    {
        public SupervisorModule(ISupervisorService supervisorService) : base(requireAuthentication: false)
        {
            Get("supervisor", args => Fetch<GetSupervisorResult, SupervisorResult>
                (async x => await supervisorService.CheckServicesAsync()).HandleAsync());
        }
    }
}