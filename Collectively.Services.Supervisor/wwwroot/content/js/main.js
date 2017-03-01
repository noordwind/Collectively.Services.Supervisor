var main = main || {};

(function () {
    main.init = function(supervisorUrl) {
      var viewModel = new DashboardViewModel(supervisorUrl);
      ko.applyBindings(viewModel);
    }

    function DashboardViewModel(supervisorUrl) {
      var self = this;
      self.result = ko.observable({});
      updateResult();

      function updateResult() {
        getSupervisorResult();
        setTimeout(updateResult, 5000);
      }

      function getSupervisorResult() {
        $.get(supervisorUrl, function(result) {
          self.result(new ResultViewModel(result));
        });
      }
    }

    function ResultViewModel(result) {
      this.services = ko.observableArray([]);
      for(var i=0; i<result.services.length; i++) {
        var service = result.services[i];
        this.services.push(new ServiceViewModel(service))
      }
    }

    function ServiceViewModel(service) {
      var self = this;
      this.name = ko.observable(service.name);
      this.checkedAt = ko.observable(service.checkedAt);
      this.type = ko.observable(service.type);
      this.description = ko.observable(service.description);
      this.alive = ko.observable(service.alive);
      this.instances = ko.observableArray([]);
      for(var i=0; i<service.instances.length; i++) {
        var instance = service.instances[i];
        this.instances.push(new ServiceInstanceViewModel(instance))
      }
    }

    function ServiceInstanceViewModel(instance) {
      this.name = ko.observable(instance.name);
      this.url = ko.observable(instance.url);
      this.browsableUrl = ko.observable(instance.browsableUrl);
      this.alive = ko.observable(instance.alive);
    }
})();