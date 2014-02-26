/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataCtrl($scope, $stateParams, Person) {
    //$scope.model = {};

    $scope.isUniqueLin = function (value) {
      if (!value) {
        return true;
      }

      return Person.query({ lin: value, exact: true })
        .$promise
        .then(function (persons) {
          return persons.length === 0 ||
            (persons.length === 1 && persons[0].id === $stateParams.id);
        });
    };

    $scope.generateLIN = function () {
      $scope.model.lin = '11731';
    };
  }

  PersonDataCtrl.$inject = ['$scope', '$stateParams', 'Person'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular));
