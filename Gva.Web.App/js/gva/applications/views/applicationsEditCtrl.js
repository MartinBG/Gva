/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    application
    ) {
    $scope.application = application;
    $scope.set = $stateParams.set;
    $scope.setPartPath = $stateParams.set + 'DocumentApplications';

    $scope.viewPerson = function (id) {
      return $state.go('root.persons.view', {
        id: id,
        appId: application.applicationId,
        set: $scope.set
      });
    };

    $scope.viewOrganization = function (id) {
      return $state.go('root.organizations.view', {
        id: id,
        appId: application.applicationId,
        set: $scope.set
      });
    };

    $scope.viewAircraft = function (id) {
      return $state.go('root.aircrafts.view', {
        id: id,
        appId: application.applicationId,
        set: $scope.set
      });
    };

    $scope.viewAirport = function (id) {
      return $state.go('root.airports.view', {
        id: id,
        appId: application.applicationId,
        set: $scope.set
      });
    };

    $scope.viewEquipment = function (id) {
      return $state.go('root.equipments.view', {
        id: id,
        appId: application.applicationId,
        set: $scope.set
      });
    };
  }

  ApplicationsEditCtrl.$inject = [
    '$stateParams',
    '$state',
    '$scope',
    'application'
  ];

  ApplicationsEditCtrl.$resolve = {
    application: [
      '$stateParams',
      'Applications',
      function ResolveApplication($stateParams, Applications) {
        return Applications.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditCtrl', ApplicationsEditCtrl);
}(angular
));
