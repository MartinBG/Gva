/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    application
    ) {
    $scope.application = application;
    $scope.set = application.lotSetAlias;

    if ($scope.application.oldDocumentNumber) {
      $scope.tabs = {
        'applications.tabs.data': 'root.applications.edit.data',
        'applications.tabs.stages': 'root.applications.edit.stages'
      };
    } else if ($scope.application.partIndex) {
      $scope.tabs = {
        'applications.tabs.data': 'root.applications.edit.data',
        'applications.tabs.case': 'root.applications.edit.case',
        'applications.tabs.stages': 'root.applications.edit.stages'
      };

      if ($scope.application.applicationTypeCode.indexOf('EX-') === 0 ||
          $scope.application.applicationTypeCode.indexOf('EX/') === 0) {
        $scope.tabs = _.assign($scope.tabs, {
          'applications.tabs.examSyst': 'root.applications.edit.examSyst'
        });
      }
    } else {
      $scope.tabs = {
        'applications.tabs.case': 'root.applications.edit.case'
      };
    }

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
}(angular, _));
