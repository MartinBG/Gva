/*global angular*/
(function (angular) {
  'use strict';

  function CertAirCarriersNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirCarrier,
    certificate
  ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.certAirCarrierForm.$validate()
        .then(function () {
          if ($scope.certAirCarrierForm.$valid) {
            return CertAirCarrier
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
    'CertAirCarrier',
    'certificate'
  ];

  CertAirCarriersNewCtrl.$resolve = {
    certificate: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('CertAirCarriersNewCtrl', CertAirCarriersNewCtrl);
}(angular));
