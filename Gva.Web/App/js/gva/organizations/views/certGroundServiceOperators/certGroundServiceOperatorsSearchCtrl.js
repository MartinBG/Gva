﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperator,
    organizationCertGroundServiceOperator
    ) {

    $scope.organizationCertGroundServiceOperator = organizationCertGroundServiceOperator;

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
    'OrganizationCertGroundServiceOperator',
    'organizationCertGroundServiceOperator'
  ];

  CertGroundServiceOperatorsSearchCtrl.$resolve = {
    organizationCertGroundServiceOperator: [
      '$stateParams',
      'OrganizationCertGroundServiceOperator',
      function ($stateParams, OrganizationCertGroundServiceOperator) {
        return OrganizationCertGroundServiceOperator.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSearchCtrl', CertGroundServiceOperatorsSearchCtrl);
}(angular));