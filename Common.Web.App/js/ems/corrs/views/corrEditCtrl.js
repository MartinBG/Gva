/*global angular*/
(function (angular) {
  'use strict';

  function CorrsEditCtrl(
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
            return Corr
              .save($scope.corr)
              .$promise
              .then(function () {
                return $state.go('root.corrs.search');
              });
          }
        });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.corrs.search');
    };
  }

  CorrsEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr',
    'corr'
  ];

  CorrsEditCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corr',
      function resolveCorr($stateParams, Corr) {
        return Corr.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
