/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsPersonSelectCtrl($scope, $state, $stateParams, Person, person) {
    $scope.filters = {
      lin: null,
      uin: null
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
      $state.go('root.applications.new.personSelect', {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectPerson = function (result) {
      person.id = result.id;
      return $state.go('^');
    };
  }

  ApplicationsPersonSelectCtrl.$inject = ['$scope', '$state', '$stateParams', 'Person', 'person'];

  angular.module('gva').controller('ApplicationsPersonSelectCtrl', ApplicationsPersonSelectCtrl);
}(angular, _));
