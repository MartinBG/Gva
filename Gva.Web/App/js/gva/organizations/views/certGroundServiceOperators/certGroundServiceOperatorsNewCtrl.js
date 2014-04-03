/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperator,
    certificate
    ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return OrganizationCertGroundServiceOperator
        .save({ id: $stateParams.id }, $scope.certificate).$promise
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
    'certificate'
  ];

  CertGroundServiceOperatorsNewCtrl.$resolve = {
    certificate: function () {
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
