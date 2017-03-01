using System.Threading.Tasks;
using Collectively.Services.Supervisor.Domain;

namespace Collectively.Services.Supervisor.Services
{
    public interface ISupervisorService
    {
         Task<SupervisorResult> CheckServicesAsync();
    }
}