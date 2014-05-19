/*global angular,_*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperational,
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
      return $scope.editCertGroundServiceOperatorsSnoOperationalForm.$validate()
        .then(function () {
          if ($scope.editCertGroundServiceOperatorsSnoOperationalForm.$valid) {
            return OrganizationCertGroundServiceOperatorsSnoOperational
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
      return OrganizationCertGroundServiceOperatorsSnoOperational
        .remove({ id: $stateParams.id, ind: certificate.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
        });
    };
  }

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperatorsSnoOperational',
    'certificate'
  ];

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperatorsSnoOperational',
      function ($stateParams, OrganizationCertGroundServiceOperatorsSnoOperational) {
        return OrganizationCertGroundServiceOperatorsSnoOperational.get({
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