/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertAirCarriersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certAirCarriers
  ) {

    $scope.certAirCarriers = _.map(certAirCarriers, function(certificate){
      certificate.services = _.pluck(certificate.part.aircarrierServices, 'name')
        .join(',</br>');
      return certificate;
    });

    $scope.editCertAirCarrier = function (cert) {
      return $state.go('root.organizations.view.certAirCarriers.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertAirCarrier = function () {
      return $state.go('root.organizations.view.certAirCarriers.new');
    };
  }

  CertAirCarriersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certAirCarriers'
  ];

  CertAirCarriersSearchCtrl.$resolve = {
    certAirCarriers: [
      '$stateParams',
      'CertAirCarriers',
      function ($stateParams, CertAirCarriers) {
        return CertAirCarriers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirCarriersSearchCtrl', CertAirCarriersSearchCtrl);
}(angular, _));