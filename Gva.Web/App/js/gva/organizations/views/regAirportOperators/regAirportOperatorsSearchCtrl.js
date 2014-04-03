﻿/*global angular*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegAirportOperator,
    organizationRegAirportOperators
    ) {

    $scope.organizationRegAirportOperators = organizationRegAirportOperators;

    $scope.editRegAirportOperator = function (address) {
      return $state.go('root.organizations.view.regAirportOperators.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.deleteRegAirportOperator = function (plan) {
      return OrganizationRegAirportOperator.remove({ id: $stateParams.id, ind: plan.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newRegAirportOperator = function () {
      return $state.go('root.organizations.view.regAirportOperators.new');
    };
  }

  RegAirportOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegAirportOperator',
    'organizationRegAirportOperators'
  ];

  RegAirportOperatorsSearchCtrl.$resolve = {
    organizationRegAirportOperators: [
      '$stateParams',
      'OrganizationRegAirportOperator',
      function ($stateParams, OrganizationRegAirportOperator) {
        return OrganizationRegAirportOperator.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegAirportOperatorsSearchCtrl', RegAirportOperatorsSearchCtrl);
}(angular));