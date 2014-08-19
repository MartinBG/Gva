/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtsFM,
    debts
  ) {
    $scope.debts = debts;
  }

  DocDebtsFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebtsFM',
    'debts'
  ];

  DocDebtsFMSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebtsFM',
      function ($stateParams, AircraftDocumentDebtsFM) {
        return AircraftDocumentDebtsFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMSearchCtrl', DocDebtsFMSearchCtrl);
}(angular));
