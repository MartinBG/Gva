/*global angular,_*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegAirportOperators,
    organizationRegAirportOperator
  ) {
    var originalOperator = _.cloneDeep(organizationRegAirportOperator);

    $scope.organizationRegAirportOperator = organizationRegAirportOperator;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationRegAirportOperator = _.cloneDeep(originalOperator);
    };

    $scope.save = function () {
      return $scope.editRegAirportOperatorForm.$validate()
      .then(function () {
        if ($scope.editRegAirportOperatorForm.$valid) {
          return OrganizationRegAirportOperators
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationRegAirportOperator)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.regAirportOperators.search');
            });
        }
      });
    };

    $scope.deleteRegAirportOperator = function () {
      return OrganizationRegAirportOperators.remove({
        id: $stateParams.id,
        ind: organizationRegAirportOperator.partIndex
      }).$promise.then(function () {
          return $state.go('root.organizations.view.regAirportOperators.search');
        });
    };
  }

  RegAirportOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRegAirportOperators',
    'organizationRegAirportOperator'
  ];

  RegAirportOperatorsEditCtrl.$resolve = {
    organizationRegAirportOperator: [
      '$stateParams',
      'OrganizationRegAirportOperators',
      function ($stateParams, OrganizationRegAirportOperators) {
        return OrganizationRegAirportOperators.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegAirportOperatorsEditCtrl', RegAirportOperatorsEditCtrl);
}(angular));
