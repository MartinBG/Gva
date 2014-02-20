/*global angular, _*/
(function (angular) {
  'use strict';

  function SelectCorrViewCtrl(
    $state,
    $stateParams,
    $scope,
    corrs,
    selectedCorrs
  ) {
    if (!selectedCorrs.onCorrSelect) {
      return $state.go('^');
    }

    $scope.corrs = _.map(corrs, function (corr) {
      var sc = _(selectedCorrs.corrs).filter({ nomValueId: corr.corrId }).first();

      if (!sc) {
        return {
          corrId: corr.corrId,
          displayName: corr.displayName,
          email: corr.email,
          correspondentType: corr.correspondentType
        };
      }
    });

    $scope.corrs = _.filter($scope.corrs, function (corr) {
      return !!corr;
    });

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
        email: $scope.filters.email
      });
    };

    $scope.selectCorr = function selectCorr(corr) {
      var nomItem = {
        nomValueId: corr.corrId,
        name: corr.displayName,
        content: corr
      };

      selectedCorrs.onCorrSelect(nomItem);
      return $state.go('^');
    };

    $scope.cancel = function cancel() {
      return $state.go('^');
    };
  }

  SelectCorrViewCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'corrs',
    'selectedCorrs'
  ];

  SelectCorrViewCtrl.$resolve = {
    corrs: [
      '$stateParams',
      'Corr',
      function resolveCorrs($stateParams, Corr) {
        return Corr.query($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('SelectCorrViewCtrl', SelectCorrViewCtrl);
}(angular, _));
