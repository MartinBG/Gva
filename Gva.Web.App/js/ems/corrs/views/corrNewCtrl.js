/*global angular*/
(function (angular) {
  'use strict';

  function CorrsNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Corr,
    corr
  ) {
    $scope.corr = corr;

    $scope.save = function save() {
      return $scope.corrForm.$validate()
        .then(function () {
          if ($scope.corrForm.$valid) {
            return Corr.save($scope.corr).$promise.then(function () {
              return $state.go('root.corrs.search');
            });
          }
        });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.corrs.search');
    };
  }

  CorrsNewCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr',
    'corr'
  ];

  CorrsNewCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corr',
      function resolveCorr($stateParams, Corr) {
        return Corr.getNew().$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsNewCtrl', CorrsNewCtrl);
}(angular));
