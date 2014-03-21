/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperator,
    certAirportOperator) {
    $scope.certAirportOperator = certAirportOperator;

    $scope.save = function () {
      return CertAirportOperator
        .save({ id: $stateParams.id }, $scope.certAirportOperator).$promise
        .then(function () {
          return $state.go('root.organizations.view.certAirportOperators.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirportOperators.search');
    };
  }

  CertAirportOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirportOperator',
    'certAirportOperator'
  ];

  CertAirportOperatorsNewCtrl.$resolve = {
    certAirportOperator: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('CertAirportOperatorsNewCtrl', CertAirportOperatorsNewCtrl);
}(angular));
