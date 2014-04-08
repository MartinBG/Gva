/*global angular*/
(function (angular) {
  'use strict';

  function PersonNewCtrl($scope, $state, Person, selectedPerson) {

    $scope.save = function () {
      return $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Person.save($scope.newPerson).$promise
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

  PersonNewCtrl.$inject = ['$scope', '$state', 'Person', 'selectedPerson'];

  angular.module('gva').controller('PersonNewCtrl', PersonNewCtrl);
}(angular));
