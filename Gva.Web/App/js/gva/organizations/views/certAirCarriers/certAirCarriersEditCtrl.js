/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirCarriersEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirCarrier,
    certificate
  ) {
    var originalCertificate = _.cloneDeep(certificate);

    $scope.certificate = certificate;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.certificate = _.cloneDeep(originalCertificate);
    };

    $scope.save = function () {
      return $scope.certAirCarrierForm.$validate()
        .then(function () {
          if ($scope.certAirCarrierForm.$valid) {
            return CertAirCarrier
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirCarriers.search');
              });
          }
        });
    };

    $scope.deleteCertAirCarrier = function () {
      return CertAirCarrier.remove({ id: $stateParams.id, ind: certificate.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.certAirCarriers.search');
        });
    };
  }

  CertAirCarriersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirCarrier',
    'certificate'
  ];

  CertAirCarriersEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirCarrier',
      function ($stateParams, CertAirCarrier) {
        return CertAirCarrier.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirCarriersEditCtrl', CertAirCarriersEditCtrl);
}(angular));
