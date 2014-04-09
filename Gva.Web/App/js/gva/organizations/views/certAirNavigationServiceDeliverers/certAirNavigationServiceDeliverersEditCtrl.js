﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirNavigationServiceDeliverer,
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
      return $scope.certAirNavigationServiceDelivererForm.$validate()
        .then(function () {
          if ($scope.certAirNavigationServiceDelivererForm.$valid) {
            return CertAirNavigationServiceDeliverer
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
              .$promise
              .then(function () {
                return $state
                  .go('root.organizations.view.certAirNavigationServiceDeliverers.search');
              });
          }
        });
    };

    $scope.deleteCertAirNavigationServiceDeliverer = function () {
      return CertAirNavigationServiceDeliverer
        .remove({ id: $stateParams.id, ind: certificate.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.certAirNavigationServiceDeliverers.search');
        });
    };
  }

  CertAirNavigationServiceDeliverersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirNavigationServiceDeliverer',
    'certificate'
  ];

  CertAirNavigationServiceDeliverersEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirNavigationServiceDeliverer',
      function ($stateParams, CertAirNavigationServiceDeliverer) {
        return CertAirNavigationServiceDeliverer.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertAirNavigationServiceDeliverersEditCtrl',
    CertAirNavigationServiceDeliverersEditCtrl);
}(angular));