/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperational,
    organizationCertGroundServiceOperatorSnoOperational) {
    $scope.certGroundServiceOperatorSnoOperational =
      organizationCertGroundServiceOperatorSnoOperational;

    $scope.save = function () {
      return OrganizationCertGroundServiceOperatorsSnoOperational
        .save({ id: $stateParams.id }, $scope.certGroundServiceOperatorSnoOperational)
        .$promise
        .then(function () {
          return $state
            .go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
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
    'OrganizationCertGroundServiceOperatorsSnoOperational',
    'organizationCertGroundServiceOperatorSnoOperational'
  ];

  CertGroundServiceOperatorsSnoOperationalNewCtrl.$resolve = {
    organizationCertGroundServiceOperatorSnoOperational: function () {
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
