/*global angular*/
(function (angular) {
  'use strict';

  function CorrsNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Corrs,
    corr
  ) {
    $scope.corr = corr;

    $scope.save = function save() {
      return $scope.corrForm.$validate()
        .then(function () {
          if ($scope.corrForm.$valid) {
            return Corrs.save($scope.corr).$promise.then(function () {
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
    'Corrs',
    'corr'
  ];

  CorrsNewCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corrs',
      function resolveCorr($stateParams, Corrs) {
        return Corrs.getNew().$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsNewCtrl', CorrsNewCtrl);
}(angular));
