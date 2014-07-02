/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationCertGroundServiceOperatorsSnoOperational
  ) {

    $scope.certGroundServiceOperatorsSnoOperational =
      organizationCertGroundServiceOperatorsSnoOperational;

    $scope.editCertGroundServiceOperatorSnoOperational = function (cert) {
      return $state.go('root.organizations.view.groundServiceOperatorsSnoOperational.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertGroundServiceOperatorSnoOperational = function () {
      return $state.go('root.organizations.view.groundServiceOperatorsSnoOperational.new');
    };
  }

  CertGroundServiceOperatorsSnoOperationalSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationCertGroundServiceOperatorsSnoOperational'
  ];

  CertGroundServiceOperatorsSnoOperationalSearchCtrl.$resolve = {
    organizationCertGroundServiceOperatorsSnoOperational: [
      '$stateParams',
      'OrganizationCertGroundServiceOperatorsSnoOperationals',
      function ($stateParams, OrganizationCertGroundServiceOperatorsSnoOperationals) {
        return OrganizationCertGroundServiceOperatorsSnoOperationals.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSnoOperationalSearchCtrl',
    CertGroundServiceOperatorsSnoOperationalSearchCtrl);
}(angular));