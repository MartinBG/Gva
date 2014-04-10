/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    Application,
    application
    ) {
    $scope.application = application;

    $scope.viewPerson = function (id) {
      return $state.go('root.persons.view', { id: id });
    };

    $scope.viewOrganization = function (id) {
      return $state.go('root.organizations.view', { id: id });
    };

    $scope.viewAircraft = function (id) {
      return $state.go('root.aircrafts.view', { id: id });
    };

    $scope.viewAirport = function (id) {
      return $state.go('root.airports.view', { id: id });
    };

    $scope.viewEquipment = function (id) {
      return $state.go('root.equipments.view', { id: id });
    };
  }

  ApplicationsEditCtrl.$inject = [
    '$stateParams',
    '$state',
    '$scope',
    'Application',
    'application'
  ];

  ApplicationsEditCtrl.$resolve = {
    application: [
      '$stateParams',
      'Application',
      function ResolveApplication($stateParams, Application) {
        return Application.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditCtrl', ApplicationsEditCtrl);
}(angular
));
