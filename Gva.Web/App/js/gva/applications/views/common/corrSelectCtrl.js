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
    $scope.corrs = _.map(corrs.correspondents, function (corr) {
      return {
        correspondentId: corr.correspondentId,
        displayName: corr.displayName,
        email: corr.email,
        correspondentTypeName: corr.correspondentTypeName
      };
    });
    $scope.corrCount = corrs.correspondentCount;
    $scope.selectedCorrs = selectedCorrs.total;

    $scope.filters = {
      displayName: null,
      email: null
    };

    _.forOwn(_.pick($stateParams, ['displayName', 'email']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.selectCorr = function selectCorr(corr) {
      selectedCorrs.current.push(corr.correspondentId);
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
        return Corr.get({
          displayName: $stateParams.displayName,
          correspondentEmail: $stateParams.email
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CorrSelectCtrl', CorrSelectCtrl);
}(angular, _));
