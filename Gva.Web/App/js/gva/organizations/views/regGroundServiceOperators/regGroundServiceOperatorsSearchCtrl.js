/*global angular*/
(function (angular) {
  'use strict';

  function RegGroundServiceOperatorsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegGroundServiceOperator,
    organizationRegGroundServiceOperators
  ) {

    $scope.organizationRegGroundServiceOperators = organizationRegGroundServiceOperators;

    $scope.editRegGroundServiceOperator = function (address) {
      return $state.go('root.organizations.view.regGroundServiceOperators.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.newRegGroundServiceOperator = function () {
      return $state.go('root.organizations.view.regGroundServiceOperators.new');
    };
  }

  RegGroundServiceOperatorsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegGroundServiceOperator',
    'organizationRegGroundServiceOperators'
  ];

  RegGroundServiceOperatorsSearchCtrl.$resolve = {
    organizationRegGroundServiceOperators: [
      '$stateParams',
      'OrganizationRegGroundServiceOperator',
      function ($stateParams, OrganizationRegGroundServiceOperator) {
        return OrganizationRegGroundServiceOperator.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegGroundServiceOperatorsSearchCtrl', RegGroundServiceOperatorsSearchCtrl);
}(angular));