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
      filter: null,
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
        filter: $scope.filters.filter,
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        lin: $scope.filters.lin
      }, { reload: true });
    };

    $scope.viewApplication = function (application) {
      return $state.go('root.applications.edit.case', { id: application.applicationId });
    };

    $scope.newApp = function () {
      return $state.go('root.applications.new', $stateParams);
    };

    $scope.linkApp = function () {
      return $state.go('root.applications.link', $stateParams);
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
