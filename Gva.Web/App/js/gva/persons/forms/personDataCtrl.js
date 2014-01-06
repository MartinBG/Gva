﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataCtrl($scope, $stateParams, Person) {
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
  }

  PersonDataCtrl.$inject = ['$scope', '$stateParams', 'Person'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular));
