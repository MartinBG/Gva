/*global angular*/
(function (angular) {
  'use strict';

  function RegGroundServiceOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegGroundServiceOperator,
    organizationRegGroundServiceOperator
  ) {
    $scope.organizationRegGroundServiceOperator = organizationRegGroundServiceOperator;

    $scope.save = function () {
      return $scope.newRegGroundServiceOperatorForm.$validate()
        .then(function () {
          if ($scope.newRegGroundServiceOperatorForm.$valid) {
            return OrganizationRegGroundServiceOperator
              .save({ id: $stateParams.id }, $scope.organizationRegGroundServiceOperator)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.regGroundServiceOperators.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.regGroundServiceOperators.search');
    };
  }

  RegGroundServiceOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegGroundServiceOperator',
    'organizationRegGroundServiceOperator'
  ];

  RegGroundServiceOperatorsNewCtrl.$resolve = {
    organizationRegGroundServiceOperator: function () {
      return {};
    }
  };

  angular.module('gva')
    .controller('RegGroundServiceOperatorsNewCtrl', RegGroundServiceOperatorsNewCtrl);
}(angular));
