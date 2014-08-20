/*global angular*/
(function (angular) {
  'use strict';

  function CertAirCarriersNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirCarriers,
    certificate
  ) {
    $scope.certificate = certificate;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.certAirCarrierForm.$validate()
        .then(function () {
          if ($scope.certAirCarrierForm.$valid) {
            return CertAirCarriers
              .save({ id: $stateParams.id }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirCarriers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirCarriers.search');
    };
  }

  CertAirCarriersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirCarriers',
    'certificate'
  ];

  CertAirCarriersNewCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirCarriers',
      function ($stateParams, CertAirCarriers) {
        return CertAirCarriers.newCertAirCarrier({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirCarriersNewCtrl', CertAirCarriersNewCtrl);
}(angular));
