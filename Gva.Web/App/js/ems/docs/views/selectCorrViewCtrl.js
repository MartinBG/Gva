/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SelectCorrViewCtrl(
    $state,
    $stateParams,
    $scope,
    corrs,
    selectedCorrs
  ) {
    $scope.corrs = _.map(corrs.correspondents, function (corr) {
      return {
        correspondentId: corr.correspondentId,
        displayName: corr.displayName,
        email: corr.email,
        correspondentTypeName: corr.correspondentTypeName
      };
    });
    $scope.corrCount = corrs.correspondentCount;
    $scope.selectedCorrs = _.map(selectedCorrs.total, function (corr) {
      return corr.nomValueId;
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
        email: $scope.filters.email,
        stamp: new Date().getTime()
      });
    };

    $scope.selectCorr = function selectCorr(corr) {
      var nomItem = {
        nomValueId: corr.correspondentId,
        name: corr.displayName
      };

      selectedCorrs.current.push(nomItem);
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
        return Corr.get(
          {
            displayName: $stateParams.displayName,
            correspondentEmail: $stateParams.email
          }).$promise;
      }
    ]
  };

  angular.module('ems').controller('SelectCorrViewCtrl', SelectCorrViewCtrl);
}(angular, _));
