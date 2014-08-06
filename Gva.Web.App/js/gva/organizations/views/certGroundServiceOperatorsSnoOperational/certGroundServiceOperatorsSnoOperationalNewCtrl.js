/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperationals,
    certificate
  ) {
    $scope.certificate = certificate;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newCertGroundServiceOperatorsSnoOperationalForm.$validate()
        .then(function () {
          if ($scope.newCertGroundServiceOperatorsSnoOperationalForm.$valid) {
            return OrganizationCertGroundServiceOperatorsSnoOperationals
              .save({ id: $stateParams.id }, $scope.certificate)
              .$promise
              .then(function () {
                return $state
                  .go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
    };
  }

  CertGroundServiceOperatorsSnoOperationalNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperatorsSnoOperationals',
    'certificate'
  ];

  CertGroundServiceOperatorsSnoOperationalNewCtrl.$resolve = {
    certificate: function () {
      return {
        part: {
          includedDocuments: [],
          ssno: []
        }
      };
    }
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSnoOperationalNewCtrl',
    CertGroundServiceOperatorsSnoOperationalNewCtrl);
}(angular));
