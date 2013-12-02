/*global angular, $*/
(function (angular) {
  'use strict';

  function PersonDataCtrl($scope, Person) {
    var self = this;

    self.$scope = $scope;

    $scope.isUniqueLin = function (value) {
      return Person.query({ lin: value, exact: true })
        .$promise
        .then(function (persons) {
          return persons.length === 0;
        });
    };
  }

  PersonDataCtrl.$inject = ['$scope', 'persons.Person'];

  angular.module('persons').controller('persons.PersonDataCtrl', PersonDataCtrl);
}(angular, $));
