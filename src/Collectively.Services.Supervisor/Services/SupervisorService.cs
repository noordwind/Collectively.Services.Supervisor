using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using Collectively.Services.Supervisor.Domain;
using Collectively.Services.Supervisor.Settings;

namespace Collectively.Services.Supervisor.Services
{
    public class SupervisorService : ISupervisorService
    {
        private static readonly ILogger Logger = Log.Logger;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly SupervisorSettings _supervisorSettings;

        public SupervisorService(SupervisorSettings supervisorSettings)
        {
            _supervisorSettings = supervisorSettings;
        }

        public async Task<SupervisorResult> CheckServicesAsync()
        {
            var result = new SupervisorResult
            {
                Services = new List<SupervisorResult.Service>()
            };

            foreach(var service in _supervisorSettings.Services)
            {
                var serviceResult = await CheckServiceAsync(service);
                result.Services.Add(serviceResult);
            }

            return result;
        }

        private async Task<SupervisorResult.Service> CheckServiceAsync(SupervisorSettings.Service service)
        {
            var result = new SupervisorResult.Service
            {
                Name = service.Name,
                Type = service.Type,
                Description = service.Description,
                Instances = new List<SupervisorResult.ServiceInstance>(),
                CheckedAt = DateTime.UtcNow
            };

            foreach(var instance in service.Instances)
            {
                var instanceResult = await CheckServiceInstanceAsync(instance);
                result.Instances.Add(instanceResult);
            }

            return result;           
        }

        private async Task<SupervisorResult.ServiceInstance> CheckServiceInstanceAsync(SupervisorSettings.ServiceInstance instance)
        {
            var result = new SupervisorResult.ServiceInstance
            {
                Name = instance.Name,
                Url = instance.Url,
                BrowsableUrl = instance.BrowsableUrl
            };

            try
            {
                var response = await _httpClient.GetAsync(result.Url);
                result.Alive = response.IsSuccessStatusCode; 
            }
            catch(Exception exception)
            {
                Logger.Error(exception, exception.ToString());
            }

            return result;
        }
    }
}