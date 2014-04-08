/*global angular,_*/
(function (angular) {
  'use strict';

  function RegAirportOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRegAirportOperator,
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
          return OrganizationRegAirportOperator
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
      return OrganizationRegAirportOperator.remove({
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
    'OrganizationRegAirportOperator',
    'organizationRegAirportOperator'
  ];

  RegAirportOperatorsEditCtrl.$resolve = {
    organizationRegAirportOperator: [
      '$stateParams',
      'OrganizationRegAirportOperator',
      function ($stateParams, OrganizationRegAirportOperator) {
        return OrganizationRegAirportOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RegAirportOperatorsEditCtrl', RegAirportOperatorsEditCtrl);
}(angular));
