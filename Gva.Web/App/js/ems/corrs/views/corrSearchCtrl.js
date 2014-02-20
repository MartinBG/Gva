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
    $scope.corrs = corrs.map(function (corr) {
      return {
        corrId: corr.corrId,
        displayName: corr.displayName,
        email: corr.email,
        correspondentType: corr.correspondentType
      };
    });

    $scope.filters = {
      displayName: null,
      email: null
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
        email: $scope.filters.email
      });
    };

    $scope.editCorr = function editCorr(corr) {
      return $state.go('root.corrs.edit', { corrId: corr.corrId });
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
        return Corr.query($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsSearchCtrl', CorrsSearchCtrl);
}(angular, _));
