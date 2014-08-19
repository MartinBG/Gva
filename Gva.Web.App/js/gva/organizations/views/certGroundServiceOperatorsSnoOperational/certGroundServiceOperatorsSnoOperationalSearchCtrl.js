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