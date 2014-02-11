/*global angular*/
(function (angular) {
  'use strict';

  function PersonsNewCtrl($scope, $state, Person) {
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

  PersonsNewCtrl.$inject = ['$scope', '$state', 'Person'];

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular));
