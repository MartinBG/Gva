/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperator,
    certificate
    ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return CertAirportOperator
        .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
        .$promise
        .then(function () {
          return $state.go('root.organizations.view.certAirportOperators.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirportOperators.search');
    };
  }

  CertAirportOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirportOperator',
    'certificate'
  ];

  CertAirportOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirportOperator',
      function ($stateParams, OrganizationAddress) {
        return OrganizationAddress.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirportOperatorsEditCtrl', CertAirportOperatorsEditCtrl);
}(angular));
