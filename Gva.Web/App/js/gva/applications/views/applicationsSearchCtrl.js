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
      $state.go('docs/search', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        regUri: $scope.filters.regUri,
        lin: $scope.filters.docName
      });
    };

    $scope.viewApplication = function (application) {
      return $state.go('applications/edit/case', { id: application.applicationId });
    };
  }

  ApplicationsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Application'];

  angular.module('gva').controller('ApplicationsSearchCtrl', ApplicationsSearchCtrl);
}(angular, _));
