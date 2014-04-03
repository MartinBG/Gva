/*global angular*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegAirportOperator,
    organizationRegAirportOperator
    ) {
    $scope.organizationRegAirportOperator = organizationRegAirportOperator;

    $scope.save = function () {
      return $scope.organizationRegAirportOperatorForm.$validate()
        .then(function () {
          if ($scope.organizationRegAirportOperatorForm.$valid) {
            return OrganizationRegAirportOperator
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
    'OrganizationRegAirportOperator',
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
