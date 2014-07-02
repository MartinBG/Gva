/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperators,
    certificate
  ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.newCertGroundServiceOperatorForm.$validate()
        .then(function () {
          if ($scope.newCertGroundServiceOperatorForm.$valid) {
            return OrganizationCertGroundServiceOperators
              .save({ id: $stateParams.id }, $scope.certificate).$promise
              .then(function () {
                return $state.go('root.organizations.view.certGroundServiceOperators.search');
              });
          }
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
    'OrganizationCertGroundServiceOperators',
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
