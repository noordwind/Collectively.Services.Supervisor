using System.Threading.Tasks;
using Coolector.Services.Supervisor.Domain;

namespace Coolector.Services.Supervisor.Services
{
    public interface ISupervisorService
    {
         Task<SupervisorResult> CheckServicesAsync();
    }
}