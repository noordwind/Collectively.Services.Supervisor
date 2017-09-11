using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using Collectively.Services.Supervisor.Domain;
using Collectively.Services.Supervisor.Settings;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Collectively.Services.Supervisor.Services
{
    public class SupervisorService : ISupervisorService
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SupervisorSettings _supervisorSettings;

        public SupervisorService(SupervisorSettings supervisorSettings)
        {
            _supervisorSettings = supervisorSettings;
        }

        public async Task<SupervisorResult> CheckServicesAsync()
        => new SupervisorResult
        {
            Services = await Task.WhenAll(_supervisorSettings.Services
                .Select(x => CheckServiceAsync(x)))
        };

        private async Task<SupervisorResult.Service> CheckServiceAsync(SupervisorSettings.Service service)
        => new SupervisorResult.Service
        {
            Name = service.Name,
            Type = service.Type,
            Description = service.Description,
            CheckedAt = DateTime.UtcNow,
            Instances = await Task.WhenAll(service.Instances
                .Select(x => CheckServiceInstanceAsync(x)))
        };        

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