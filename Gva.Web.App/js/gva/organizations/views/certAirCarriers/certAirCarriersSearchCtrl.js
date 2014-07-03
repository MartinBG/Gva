/*global angular*/
(function (angular) {
  'use strict';

  function CertAirCarriersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certAirCarriers
  ) {

    $scope.certAirCarriers = certAirCarriers;

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
}(angular));