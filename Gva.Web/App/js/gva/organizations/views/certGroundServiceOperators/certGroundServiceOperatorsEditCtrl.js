/*global angular,_*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperator,
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
      return $scope.editCertGroundServiceOperatorForm.$validate()
        .then(function () {
          if ($scope.editCertGroundServiceOperatorForm.$valid) {
            return OrganizationCertGroundServiceOperator
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
      return OrganizationCertGroundServiceOperator
        .remove({ id: $stateParams.id, ind: certificate.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.certGroundServiceOperators.search');
        });
    };
  }

  CertGroundServiceOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperator',
    'certificate'
  ];

  CertGroundServiceOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperator',
      function ($stateParams, OrganizationCertGroundServiceOperator) {
        return OrganizationCertGroundServiceOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsEditCtrl', CertGroundServiceOperatorsEditCtrl);
}(angular));