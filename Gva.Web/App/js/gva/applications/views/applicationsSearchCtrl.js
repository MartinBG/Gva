/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    applications
    ) {
    $scope.applications = applications;

    $scope.filters = {
      fromDate: null,
      toDate: null,
      regUri: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      return $state.go('root.applications.search', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        lin: $scope.filters.lin
      });
    };

    $scope.viewApplication = function (application) {
      if (application.docId) {
        return $state.go('root.applications.edit.case', { id: application.applicationId });
      }
      else {
        return $state.go('root.persons.view.documentApplications.edit', {
          id: application.personId,
          ind: application.appPartIndex
        });
      }
    };

    $scope.newApp = function () {
      return $state.go('root.applications.new');
    };

    $scope.linkApp = function () {
      return $state.go('root.applications.link');
    };
  }

  ApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Application',
    'applications'
  ];

  ApplicationsSearchCtrl.$resolve = {
    applications: [
      '$stateParams',
      'Application',
      function ResolveApps($stateParams, Application) {
        return Application.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsSearchCtrl', ApplicationsSearchCtrl);
}(angular, _));
