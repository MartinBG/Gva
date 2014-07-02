/*global angular*/
(function (angular) {
  'use strict';

  function PersonNewCtrl($scope, $state, Persons, selectedPerson) {
    var personData = {
      firstName: $state.payload ? $state.payload.firstName : null,
      lastName: $state.payload ? $state.payload.lastName : null,
      uin: $state.payload ? $state.payload.uin : null
    };

    $scope.newPerson = {
      personData: personData
    };

    $scope.save = function () {
      return $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Persons.save($scope.newPerson).$promise
            .then(function (person) {
              selectedPerson.push(person.id);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  PersonNewCtrl.$inject = ['$scope', '$state', 'Persons', 'selectedPerson'];

  angular.module('gva').controller('PersonNewCtrl', PersonNewCtrl);
}(angular));
