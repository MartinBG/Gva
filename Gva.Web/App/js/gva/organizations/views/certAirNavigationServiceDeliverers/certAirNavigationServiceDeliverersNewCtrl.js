/*global angular*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirNavigationServiceDeliverer,
    certificate
  ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.certAirNavigationServiceDelivererForm.$validate()
        .then(function () {
          if ($scope.certAirNavigationServiceDelivererForm.$valid) {
            return CertAirNavigationServiceDeliverer
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
    'CertAirNavigationServiceDeliverer',
    'certificate'
  ];

  CertAirNavigationServiceDeliverersNewCtrl.$resolve = {
    certificate: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva')
    .controller('CertAirNavigationServiceDeliverersNewCtrl',
    CertAirNavigationServiceDeliverersNewCtrl);
}(angular));
