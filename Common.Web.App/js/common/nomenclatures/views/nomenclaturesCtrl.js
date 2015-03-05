/*global angular*/
(function (angular) {
  'use strict';

  function NomenclaturesCtrl(
    $scope,
    $state,
    $stateParams,
    NomenclaturesRsrc,
    nomenclatures
  ) {
    $scope.nomenclatures = nomenclatures;
  }

  NomenclaturesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'NomenclaturesRsrc',
    'nomenclatures'
  ];

  NomenclaturesCtrl.$resolve = {
    nomenclatures: [
      'NomenclaturesRsrc',
      function resolveNomenclatures(NomenclaturesRsrc) {
        return NomenclaturesRsrc.query().$promise;
      }
    ]
  };

  angular.module('common').controller('NomenclaturesCtrl', NomenclaturesCtrl);
}(angular));
