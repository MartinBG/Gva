/*global angular*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirNavigationServiceDeliverer,
    certAirNavigationServiceDeliverers
  ) {

    $scope.certAirNavigationServiceDeliverers = certAirNavigationServiceDeliverers;

    $scope.editCertAirNavigationServiceDeliverer = function (cert) {
      return $state.go('root.organizations.view.certAirNavigationServiceDeliverers.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertAirNavigationServiceDeliverer = function () {
      return $state.go('root.organizations.view.certAirNavigationServiceDeliverers.new');
    };
  }

  CertAirNavigationServiceDeliverersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirNavigationServiceDeliverer',
    'certAirNavigationServiceDeliverers'
  ];

  CertAirNavigationServiceDeliverersSearchCtrl.$resolve = {
    certAirNavigationServiceDeliverers: [
      '$stateParams',
      'CertAirNavigationServiceDeliverer',
      function ($stateParams, CertAirNavigationServiceDeliverer) {
        return CertAirNavigationServiceDeliverer.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirNavigationServiceDeliverersSearchCtrl',
    CertAirNavigationServiceDeliverersSearchCtrl);
}(angular));