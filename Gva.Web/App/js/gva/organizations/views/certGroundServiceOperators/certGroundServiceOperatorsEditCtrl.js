/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperator,
    certificate
    ) {

    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.certGroundServiceOperatorForm.$validate()
        .then(function () {
          if ($scope.certGroundServiceOperatorForm.$valid) {
            return OrganizationCertGroundServiceOperator
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.certificate)
              .$promise
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

  CertGroundServiceOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperator',
    'certificate'
  ];

  CertGroundServiceOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperator',
      function ($stateParams, OrganizationCertGroundServiceOperator) {
        return OrganizationCertGroundServiceOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsEditCtrl', CertGroundServiceOperatorsEditCtrl);
}(angular));