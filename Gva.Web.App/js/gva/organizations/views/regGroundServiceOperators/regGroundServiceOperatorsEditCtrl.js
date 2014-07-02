﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function RegGroundServiceOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegGroundServiceOperators,
    organizationRegGroundServiceOperator
  ) {
    var originalOperator = _.cloneDeep(organizationRegGroundServiceOperator);

    $scope.organizationRegGroundServiceOperator = organizationRegGroundServiceOperator;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationRegGroundServiceOperator = _.cloneDeep(originalOperator);
    };

    $scope.save = function () {
      return $scope.editRegGroundServiceOperatorForm.$validate()
      .then(function () {
        if ($scope.editRegGroundServiceOperatorForm.$valid) {
          return OrganizationRegGroundServiceOperators
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationRegGroundServiceOperator)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.regGroundServiceOperators.search');
            });
        }
      });
    };

    $scope.deleteRegGroundServiceOperator = function () {
      return OrganizationRegGroundServiceOperators
        .remove({
          id: $stateParams.id,
          ind: organizationRegGroundServiceOperator.partIndex
        }).$promise.then(function () {
          return $state.go('root.organizations.view.regGroundServiceOperators.search');
        });
    };
  }

  RegGroundServiceOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegGroundServiceOperators',
    'organizationRegGroundServiceOperator'
  ];

  RegGroundServiceOperatorsEditCtrl.$resolve = {
    organizationRegGroundServiceOperator: [
      '$stateParams',
      'OrganizationRegGroundServiceOperators',
      function ($stateParams, OrganizationRegGroundServiceOperators) {
        return OrganizationRegGroundServiceOperators.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegGroundServiceOperatorsEditCtrl', RegGroundServiceOperatorsEditCtrl);
}(angular));
