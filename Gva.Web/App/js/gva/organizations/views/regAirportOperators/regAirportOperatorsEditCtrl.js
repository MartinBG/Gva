/*global angular*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsEditCtrl(
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
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationRegAirportOperator)
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

  RegAirportOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegAirportOperator',
    'organizationRegAirportOperator'
  ];

  RegAirportOperatorsEditCtrl.$resolve = {
    organizationRegAirportOperator: [
      '$stateParams',
      'OrganizationRegAirportOperator',
      function ($stateParams, OrganizationRegAirportOperator) {
        return OrganizationRegAirportOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegAirportOperatorsEditCtrl', RegAirportOperatorsEditCtrl);
}(angular));
