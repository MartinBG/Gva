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
