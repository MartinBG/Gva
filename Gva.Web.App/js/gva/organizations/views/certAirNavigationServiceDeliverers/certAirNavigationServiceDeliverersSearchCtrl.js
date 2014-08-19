/*global angular*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certAirNavigationServiceDeliverers
  ) {
    $scope.certAirNavigationServiceDeliverers = certAirNavigationServiceDeliverers;
  }

  CertAirNavigationServiceDeliverersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certAirNavigationServiceDeliverers'
  ];

  CertAirNavigationServiceDeliverersSearchCtrl.$resolve = {
    certAirNavigationServiceDeliverers: [
      '$stateParams',
      'CertAirNavigationServiceDeliverers',
      function ($stateParams, CertAirNavigationServiceDeliverers) {
        return CertAirNavigationServiceDeliverers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirNavigationServiceDeliverersSearchCtrl',
    CertAirNavigationServiceDeliverersSearchCtrl);
}(angular));