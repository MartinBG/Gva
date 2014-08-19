/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebts,
    debts
  ) {
    $scope.debts = debts;
  }

  DocDebtsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebts',
    'debts'
  ];

  DocDebtsSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebts',
      function ($stateParams, AircraftDocumentDebts) {
        return AircraftDocumentDebts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsSearchCtrl', DocDebtsSearchCtrl);
}(angular));
