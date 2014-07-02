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