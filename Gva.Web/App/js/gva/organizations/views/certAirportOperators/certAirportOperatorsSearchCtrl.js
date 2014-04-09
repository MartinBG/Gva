/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertAirportOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperator,
    certAirportOperators
  ) {

    $scope.certAirportOperators = _.map(certAirportOperators, function(certificate){
      certificate.activities = _.pluck(certificate.part.airportoperatorActivityTypes, 'name')
        .join(',</br>');
      return certificate;
    });

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
}(angular, _));