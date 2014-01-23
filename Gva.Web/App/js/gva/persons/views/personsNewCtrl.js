﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonsNewCtrl($scope, $state, Person) {
    $scope.save = function () {
      return Person.save($scope.newPerson).$promise
      .then(function () {
        return $state.go('persons.search');
      });
    };

    $scope.cancel = function () {
      return $state.go('persons.search');
    };
  }

  PersonsNewCtrl.$inject = ['$scope', '$state', 'Person'];

  angular.module('gva').controller('PersonsNewCtrl', PersonsNewCtrl);
}(angular));