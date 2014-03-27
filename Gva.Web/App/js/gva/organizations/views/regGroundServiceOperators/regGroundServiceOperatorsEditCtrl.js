/*global angular*/
(function (angular) {
  'use strict';

  function RegGroundServiceOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegGroundServiceOperator,
    organizationRegGroundServiceOperator) {
    $scope.organizationRegGroundServiceOperator = organizationRegGroundServiceOperator;

    $scope.save = function () {
      return $scope.organizationRegGroundServiceOperatorForm.$validate()
      .then(function () {
        if ($scope.organizationRegGroundServiceOperatorForm.$valid) {
          return OrganizationRegGroundServiceOperator
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationRegGroundServiceOperator)
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

  RegGroundServiceOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegGroundServiceOperator',
    'organizationRegGroundServiceOperator'
  ];

  RegGroundServiceOperatorsEditCtrl.$resolve = {
    organizationRegGroundServiceOperator: [
      '$stateParams',
      'OrganizationRegGroundServiceOperator',
      function ($stateParams, OrganizationRegGroundServiceOperator) {
        return OrganizationRegGroundServiceOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegGroundServiceOperatorsEditCtrl', RegGroundServiceOperatorsEditCtrl);
}(angular));
