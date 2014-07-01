/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Aops,
    apps
  ) {
    $scope.apps = apps.applications;
    $scope.appCount = apps.applicationCount;

    //$scope.filters = {
    //  displayName: null,
    //  correspondentEmail: null
    //};

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newApp = function newCorr() {
      return Aops.getNew()
        .$promise
        .then(function (data) {
          return $state.go('root.apps.edit', { id: data.aopApplicationId });
        });
    };

    $scope.search = function search() {
      return $state.go($state.current, {
        //displayName: $scope.filters.displayName,
        //correspondentEmail: $scope.filters.correspondentEmail
      }, { reload: true });
    };

    $scope.editApp = function editCorr(app) {
      return $state.go('root.apps.edit', { id: app.aopApplicationId });
    };
  }

  AppsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aops',
    'apps'
  ];

  AppsSearchCtrl.$resolve = {
    apps: [
      '$stateParams',
      'Aops',
      function resolveApps($stateParams, Aops) {
        return Aops.get($stateParams).$promise;
      }
    ]
  };

  angular.module('aop').controller('AppsSearchCtrl', AppsSearchCtrl);
}(angular, _));
