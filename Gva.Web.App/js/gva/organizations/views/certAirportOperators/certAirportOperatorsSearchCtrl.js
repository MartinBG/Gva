/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certAirportOperators
  ) {
    $scope.certAirportOperators = certAirportOperators;
  }

  CertAirportOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certAirportOperators'
  ];

  CertAirportOperatorsSearchCtrl.$resolve = {
    certAirportOperators: [
      '$stateParams',
      'CertAirportOperators',
      function ($stateParams, CertAirportOperators) {
        return CertAirportOperators.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirportOperatorsSearchCtrl', CertAirportOperatorsSearchCtrl);
}(angular));
