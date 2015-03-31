/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirCarriersEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirCarriers,
    certificate,
    scMessage
  ) {
    var originalCertificate = _.cloneDeep(certificate);

    $scope.certificate = certificate;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

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
            return CertAirCarriers
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirCarriers.search');
              });
          }
        });
    };

    $scope.deleteCertAirCarrier = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return CertAirCarriers.remove({ id: $stateParams.id, ind: $stateParams.ind })
            .$promise.then(function () {
              return $state.go('root.organizations.view.certAirCarriers.search');
            });
        }
      });
    };
  }

  CertAirCarriersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirCarriers',
    'certificate',
    'scMessage'
  ];

  CertAirCarriersEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirCarriers',
      function ($stateParams, CertAirCarriers) {
        return CertAirCarriers.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirCarriersEditCtrl', CertAirCarriersEditCtrl);
}(angular));
