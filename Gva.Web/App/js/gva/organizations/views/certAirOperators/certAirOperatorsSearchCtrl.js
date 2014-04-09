/*global angular*/
(function (angular) {
  'use strict';

  function CertAirOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirOperator,
    certAirOperators
  ) {

    $scope.certAirOperators = certAirOperators;

    $scope.editCertAirOperator = function (cert) {
      return $state.go('root.organizations.view.certAirOperators.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertAirOperator = function () {
      return $state.go('root.organizations.view.certAirOperators.new');
    };
  }

  CertAirOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirOperator',
    'certAirOperators'
  ];

  CertAirOperatorsSearchCtrl.$resolve = {
    certAirOperators: [
      '$stateParams',
      'CertAirOperator',
      function ($stateParams, CertAirOperator) {
        return CertAirOperator.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirOperatorsSearchCtrl', CertAirOperatorsSearchCtrl);
}(angular));