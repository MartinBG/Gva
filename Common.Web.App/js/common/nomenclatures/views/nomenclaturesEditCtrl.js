/*global angular*/
(function (angular) {
  'use strict';

  function NomenclaturesEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    NomenclaturesRsrc,
    nomenclature
  ) {
    $scope.nomenclature = nomenclature;

    $scope.save = function save() {
      return $scope.nomenclatureForm.$validate().then(function () {
        if ($scope.nomenclatureForm.$valid) {
          return NomenclaturesRsrc.save({ id: $stateParams.id }, $scope.nomenclature)
            .$promise.then(function () {
              return $state.go('root.nomenclatures.search');
            });
        }
      });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.nomenclatures.search');
    };
  }

  NomenclaturesEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'NomenclaturesRsrc',
    'nomenclature'
  ];

  NomenclaturesEditCtrl.$resolve = {
    nomenclature: [
      '$stateParams',
      'NomenclaturesRsrc',
      function resolveUnit($stateParams, NomenclaturesRsrc) {
        if ($stateParams.id) {
          return NomenclaturesRsrc.get({ id: $stateParams.id }).$promise;
        } else {
          return {};
        }
      }
    ]
  };

  angular.module('common').controller('NomenclaturesEditCtrl', NomenclaturesEditCtrl);
}(angular));
