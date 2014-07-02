/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationCertGroundServiceOperators
  ) {

    $scope.organizationCertGroundServiceOperators = organizationCertGroundServiceOperators;

    $scope.editCertGroundServiceOperator = function (cert) {
      return $state.go('root.organizations.view.certGroundServiceOperators.edit', {
        id: $stateParams.id,
        ind: cert.partIndex
      });
    };

    $scope.newCertGroundServiceOperator = function () {
      return $state.go('root.organizations.view.certGroundServiceOperators.new');
    };
  }

  CertGroundServiceOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationCertGroundServiceOperators'
  ];

  CertGroundServiceOperatorsSearchCtrl.$resolve = {
    organizationCertGroundServiceOperators: [
      '$stateParams',
      'OrganizationCertGroundServiceOperators',
      function ($stateParams, OrganizationCertGroundServiceOperators) {
        return OrganizationCertGroundServiceOperators.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSearchCtrl', CertGroundServiceOperatorsSearchCtrl);
}(angular));