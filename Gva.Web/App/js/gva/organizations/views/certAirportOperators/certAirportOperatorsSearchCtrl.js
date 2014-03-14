/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperator,
    certAirportOperators) {

    $scope.certAirportOperators = certAirportOperators;

    $scope.editCertAirportOperator = function (cert) {
      return $state.go('root.organizations.view.certAirportOperators.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.deleteCertAirportOperator = function (cert) {
      return CertAirportOperator.remove({ id: $stateParams.id, ind: cert.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'CertAirportOperator',
    'certAirportOperators'
  ];

  CertAirportOperatorsSearchCtrl.$resolve = {
    certAirportOperators: [
      '$stateParams',
      'CertAirportOperator',
      function ($stateParams, CertAirportOperator) {
        return CertAirportOperator.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirportOperatorsSearchCtrl', CertAirportOperatorsSearchCtrl);
}(angular));