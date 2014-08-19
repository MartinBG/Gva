/*global angular*/
(function (angular) {
  'use strict';

  function RegGroundServiceOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationRegGroundServiceOperators
  ) {
    $scope.organizationRegGroundServiceOperators = organizationRegGroundServiceOperators;
  }

  RegGroundServiceOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationRegGroundServiceOperators'
  ];

  RegGroundServiceOperatorsSearchCtrl.$resolve = {
    organizationRegGroundServiceOperators: [
      '$stateParams',
      'OrganizationRegGroundServiceOperators',
      function ($stateParams, OrganizationRegGroundServiceOperators) {
        return OrganizationRegGroundServiceOperators.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegGroundServiceOperatorsSearchCtrl', RegGroundServiceOperatorsSearchCtrl);
}(angular));
