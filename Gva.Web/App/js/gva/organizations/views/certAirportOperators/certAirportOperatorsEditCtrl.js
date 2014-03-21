/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperator,
    certAirportOperator
  ) {
    $scope.certAirportOperator = certAirportOperator;

    $scope.save = function () {
      return CertAirportOperator
        .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certAirportOperator)
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
    'certAirportOperator'
  ];

  CertAirportOperatorsEditCtrl.$resolve = {
    certAirportOperator: [
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
