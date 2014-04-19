/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrSelectCtrl(
    $state,
    $stateParams,
    $scope,
    corrs,
    selectedCorrs
  ) {
    if (!selectedCorrs.onCorrSelect) {
      return $state.go('^');
    }

    $scope.corrs = _.map(corrs.correspondents, function (corr) {
      return {
        correspondentId: corr.correspondentId,
        displayName: corr.displayName,
        email: corr.email,
        correspondentTypeName: corr.correspondentTypeName
      };
    });
    $scope.corrCount = corrs.correspondentCount;
    $scope.selectedCorrs = selectedCorrs.corrs;

    $scope.filters = {
      displayName: null,
      email: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function Search() {
      return $state.go($state.current, {
        displayName: $scope.filters.displayName,
        email: $scope.filters.email,
        stamp: new Date().getTime()
      });
    };

    $scope.selectCorr = function selectCorr(corr) {
      selectedCorrs.onCorrSelect(corr.correspondentId);
      return $state.go('^');
    };

    $scope.cancel = function cancel() {
      return $state.go('^');
    };
  }

  CorrSelectCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'corrs',
    'selectedCorrs'
  ];

  CorrSelectCtrl.$resolve = {
    corrs: [
      '$stateParams',
      'Corr',
      function resolveCorrs($stateParams, Corr) {
        return Corr.get(
          {
            displayName: $stateParams.displayName,
            correspondentEmail: $stateParams.email
          }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CorrSelectCtrl', CorrSelectCtrl);
}(angular, _));
