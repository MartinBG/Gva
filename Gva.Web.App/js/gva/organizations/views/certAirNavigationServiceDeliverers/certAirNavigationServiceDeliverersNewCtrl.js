/*global angular*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirNavigationServiceDeliverers,
    certificate
  ) {
    $scope.certificate = certificate;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.certAirNavigationServiceDelivererForm.$validate()
        .then(function () {
          if ($scope.certAirNavigationServiceDelivererForm.$valid) {
            return CertAirNavigationServiceDeliverers
              .save({ id: $stateParams.id }, $scope.certificate)
              .$promise
              .then(function () {
                return $state
                  .go('root.organizations.view.certAirNavigationServiceDeliverers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirNavigationServiceDeliverers.search');
    };
  }

  CertAirNavigationServiceDeliverersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirNavigationServiceDeliverers',
    'certificate'
  ];
  
  CertAirNavigationServiceDeliverersNewCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirNavigationServiceDeliverers',
      function ($stateParams, CertAirNavigationServiceDeliverers) {
        return CertAirNavigationServiceDeliverers.newCertAirNavigationServiceDeliverer({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirNavigationServiceDeliverersNewCtrl',
    CertAirNavigationServiceDeliverersNewCtrl);
}(angular));
