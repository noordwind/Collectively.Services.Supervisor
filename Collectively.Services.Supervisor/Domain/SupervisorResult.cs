using System;
using System.Collections.Generic;
using System.Linq;

namespace Collectively.Services.Supervisor.Domain
{
    public class SupervisorResult
    {
        public IList<Service> Services { get; set; }

        public class Service 
        {
            public string Name { get; set;}
            public string Type { get; set; }
            public string Description { get; set; }
            public DateTime CheckedAt { get; set; }
            public bool Alive => Instances.Any(x => x.Alive);
            public IList<ServiceInstance> Instances { get; set; }
        }

        public class ServiceInstance 
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string BrowsableUrl { get; set; }
            public bool Alive { get; set; }
        }        
    }
}