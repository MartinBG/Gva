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