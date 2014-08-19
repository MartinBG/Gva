/*global angular*/
(function (angular) {
  'use strict';

  function CertAirOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certAirOperators
  ) {
    $scope.certAirOperators = certAirOperators;
  }

  CertAirOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certAirOperators'
  ];

  CertAirOperatorsSearchCtrl.$resolve = {
    certAirOperators: [
      '$stateParams',
      'CertAirOperators',
      function ($stateParams, CertAirOperators) {
        return CertAirOperators.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirOperatorsSearchCtrl', CertAirOperatorsSearchCtrl);
}(angular));
