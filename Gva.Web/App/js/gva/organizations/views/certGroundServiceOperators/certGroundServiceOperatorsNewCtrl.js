/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperator,
    organizationCertGroundServiceOperator) {
    $scope.organizationCertGroundServiceOperator = organizationCertGroundServiceOperator;

    $scope.save = function () {
      return OrganizationCertGroundServiceOperator
        .save({ id: $stateParams.id }, $scope.organizationCertGroundServiceOperator).$promise
        .then(function () {
          return $state.go('root.organizations.view.certGroundServiceOperators.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certGroundServiceOperators.search');
    };
  }

  CertGroundServiceOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperator',
    'organizationCertGroundServiceOperator'
  ];

  CertGroundServiceOperatorsNewCtrl.$resolve = {
    organizationCertGroundServiceOperator: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsNewCtrl', CertGroundServiceOperatorsNewCtrl);
}(angular));
