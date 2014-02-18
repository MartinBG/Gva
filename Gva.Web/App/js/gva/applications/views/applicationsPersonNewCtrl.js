/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsPersonNewCtrl($scope, $state, Person, selectedPerson) {

    $scope.save = function () {
      $scope.newPersonForm.$validate()
      .then(function () {
        if ($scope.newPersonForm.$valid) {
          return Person.save($scope.newPerson).$promise
            .then(function (result) {
              selectedPerson.push(result.lotId);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  ApplicationsPersonNewCtrl.$inject = ['$scope', '$state', 'Person', 'selectedPerson'];

  angular.module('gva').controller('ApplicationsPersonNewCtrl', ApplicationsPersonNewCtrl);
}(angular));
