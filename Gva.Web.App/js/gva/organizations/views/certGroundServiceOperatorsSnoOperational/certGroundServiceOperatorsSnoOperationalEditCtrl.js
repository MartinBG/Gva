/*global angular,_*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperationals,
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
      return $scope.editCertGroundServiceOperatorsSnoOperationalForm.$validate()
        .then(function () {
          if ($scope.editCertGroundServiceOperatorsSnoOperationalForm.$valid) {
            return OrganizationCertGroundServiceOperatorsSnoOperationals
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.certificate)
              .$promise
              .then(function () {
                return $state
                  .go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
              });
          }
        });
    };

    $scope.deleteCert = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationCertGroundServiceOperatorsSnoOperationals
            .remove({ id: $stateParams.id, ind: certificate.partIndex })
            .$promise.then(function () {
              return $state
                .go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
            });
        }
      });
    };
  }

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperatorsSnoOperationals',
    'certificate',
    'scMessage'
  ];

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperatorsSnoOperationals',
      function ($stateParams, OrganizationCertGroundServiceOperatorsSnoOperationals) {
        return OrganizationCertGroundServiceOperatorsSnoOperationals.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSnoOperationalEditCtrl',
    CertGroundServiceOperatorsSnoOperationalEditCtrl);
}(angular));
