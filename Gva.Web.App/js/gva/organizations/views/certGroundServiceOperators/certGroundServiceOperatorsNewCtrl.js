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
    $scope.lotId = $stateParams.id;

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
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperators',
      function ($stateParams, OrganizationCertGroundServiceOperators) {
        return OrganizationCertGroundServiceOperators.newCertGroundServiceOperator({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsNewCtrl', CertGroundServiceOperatorsNewCtrl);
}(angular));
