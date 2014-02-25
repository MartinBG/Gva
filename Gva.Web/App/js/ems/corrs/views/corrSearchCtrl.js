/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Corr,
    corrs
  ) {
    $scope.corrs = corrs.correspondents.map(function (corr) {
      return {
        correspondentId: corr.correspondentId,
        displayName: corr.displayName,
        email: corr.email,
        correspondentTypeName: corr.correspondentTypeName
      };
    });

    $scope.corrCount = corrs.correspondentCount;

    $scope.filters = {
      displayName: null,
      correspondentEmail: null
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newCorr = function newCorr() {
      return $state.go('root.corrs.new');
    };

    $scope.search = function search() {
      return $state.go('root.corrs.search', {
        displayName: $scope.filters.displayName,
        correspondentEmail: $scope.filters.correspondentEmail
      });
    };

    $scope.editCorr = function editCorr(corr) {
      return $state.go('root.corrs.edit', { id: corr.correspondentId });
    };
  }

  CorrsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Corr',
    'corrs'
  ];

  CorrsSearchCtrl.$resolve = {
    corrs: [
      '$stateParams',
      'Corr',
      function resolveCorrs($stateParams, Corr) {
        return Corr.get($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsSearchCtrl', CorrsSearchCtrl);
}(angular, _));
