/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonSelectCtrl($scope, $state, $stateParams, Person, selectedPerson) {
    $scope.filters = {
      lin: null,
      uin: null,
      names: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Person.query($stateParams).$promise.then(function (persons) {
      $scope.persons = persons;
    });

    $scope.search = function () {
      $state.go($state.current, {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization
      }, { reload: true });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectPerson = function (result) {
      selectedPerson.push(result.id);
      return $state.go('^');
    };

    $scope.viewPerson = function (result) {
      return $state.go('root.persons.view.edit', { id: result.id });
    };
  }

  PersonSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Person',
    'selectedPerson'
  ];

  angular.module('gva').controller('PersonSelectCtrl', PersonSelectCtrl);
}(angular, _));
