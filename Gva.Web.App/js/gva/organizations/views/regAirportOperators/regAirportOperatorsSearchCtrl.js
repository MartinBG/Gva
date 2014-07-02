/*global angular*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationRegAirportOperators
  ) {

    $scope.organizationRegAirportOperators = organizationRegAirportOperators;

    $scope.editRegAirportOperator = function (address) {
      return $state.go('root.organizations.view.regAirportOperators.edit', {
        id: $stateParams.id,
        ind: address.partIndex
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
    'organizationRegAirportOperators'
  ];

  RegAirportOperatorsSearchCtrl.$resolve = {
    organizationRegAirportOperators: [
      '$stateParams',
      'OrganizationRegAirportOperators',
      function ($stateParams, OrganizationRegAirportOperators) {
        return OrganizationRegAirportOperators.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegAirportOperatorsSearchCtrl', RegAirportOperatorsSearchCtrl);
}(angular));