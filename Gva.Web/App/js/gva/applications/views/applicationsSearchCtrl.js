/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsSearchCtrl($scope, $state, $stateParams, Application) {
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

    Application.query($stateParams).$promise.then(function (applications) {
      $scope.applications = applications;
    });

    $scope.search = function () {
      $state.go('root.applications.search', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        lin: $scope.filters.lin,
        regUri: $scope.filters.regUri
      });
    };

    $scope.viewApplication = function (application) {
      return $state.go('root.applications.edit.case', { id: application.applicationId });
    };
  }

  ApplicationsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Application'];

  angular.module('gva').controller('ApplicationsSearchCtrl', ApplicationsSearchCtrl);
}(angular, _));
