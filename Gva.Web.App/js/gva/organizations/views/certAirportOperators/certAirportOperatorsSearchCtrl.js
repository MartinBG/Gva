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

    $scope.editCertAirportOperator = function (cert) {
      return $state.go('root.organizations.view.certAirportOperators.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertAirportOperator = function () {
      return $state.go('root.organizations.view.certAirportOperators.new');
    };
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