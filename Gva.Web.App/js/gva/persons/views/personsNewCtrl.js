/*global angular*/
(function (angular) {
  'use strict';

  function PersonsNewCtrl($scope, $state, Persons, person) {
    $scope.newPerson = person;

    $scope.save = function () {
      return $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Persons.save($scope.newPerson).$promise
            .then(function (result) {
              return $state.go('root.persons.view.licences.search', { id: result.id });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.search');
    };
  }

  PersonsNewCtrl.$inject = ['$scope', '$state', 'Persons', 'person'];

  PersonsNewCtrl.$resolve = {
    person: [
      'Persons',
      function (Persons) {
        return Persons.newPerson({extendedVersion : false}).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular));
