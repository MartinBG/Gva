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
