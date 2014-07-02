/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirNavigationServiceDeliverersEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirNavigationServiceDeliverers,
    certificate
  ) {
    var originalCertificate = _.cloneDeep(certificate);

    $scope.certificate = certificate;
    $scope.editMode = null;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

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
            return CertAirNavigationServiceDeliverers
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
      return CertAirNavigationServiceDeliverers
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
    'CertAirNavigationServiceDeliverers',
    'certificate'
  ];

  CertAirNavigationServiceDeliverersEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirNavigationServiceDeliverers',
      function ($stateParams, CertAirNavigationServiceDeliverers) {
        return CertAirNavigationServiceDeliverers.get({
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
