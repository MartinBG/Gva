/*global angular*/
(function (angular) {
  'use strict';

  function PersonsNewCtrl($scope, $state, Person, person) {
    $scope.newPerson = person;

    $scope.save = function () {
      $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Person.save($scope.newPerson).$promise
            .then(function () {
              return $state.go('root.persons.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.search');
    };
  }

  PersonsNewCtrl.$inject = ['$scope', '$state', 'Person', 'person'];

  PersonsNewCtrl.$resolve = {
    person: function () {
      return {
        personData: {},
        personDocumentId: {},
        personAddress: {}
      };
    }
  };

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular));
