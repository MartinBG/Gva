/*global angular,_*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperators,
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
      return $scope.editCertGroundServiceOperatorForm.$validate()
        .then(function () {
          if ($scope.editCertGroundServiceOperatorForm.$valid) {
            return OrganizationCertGroundServiceOperators
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certGroundServiceOperators.search');
              });
          }
        });
    };

    $scope.deleteCertGroundServiceOperator = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationCertGroundServiceOperators
            .remove({ id: $stateParams.id, ind: certificate.partIndex })
            .$promise.then(function () {
              return $state.go('root.organizations.view.certGroundServiceOperators.search');
            });
        }
      });
    };
  }

  CertGroundServiceOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperators',
    'certificate',
    'scMessage'
  ];

  CertGroundServiceOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperators',
      function ($stateParams, OrganizationCertGroundServiceOperators) {
        return OrganizationCertGroundServiceOperators.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsEditCtrl', CertGroundServiceOperatorsEditCtrl);
}(angular));
