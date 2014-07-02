/*global angular*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegAirportOperators,
    organizationRegAirportOperator
  ) {
    $scope.organizationRegAirportOperator = organizationRegAirportOperator;

    $scope.save = function () {
      return $scope.newRegAirportOperatorForm.$validate()
        .then(function () {
          if ($scope.newRegAirportOperatorForm.$valid) {
            return OrganizationRegAirportOperators
              .save({ id: $stateParams.id }, $scope.organizationRegAirportOperator)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.regAirportOperators.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.regAirportOperators.search');
    };
  }

  RegAirportOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegAirportOperators',
    'organizationRegAirportOperator'
  ];

  RegAirportOperatorsNewCtrl.$resolve = {
    organizationRegAirportOperator: function () {
      return {};
    }
  };

  angular.module('gva')
    .controller('RegAirportOperatorsNewCtrl', RegAirportOperatorsNewCtrl);
}(angular));
