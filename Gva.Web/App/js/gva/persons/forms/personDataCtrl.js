/*global angular, $*/
(function (angular) {
  'use strict';

  function PersonDataCtrl($scope, Person) {
    var self = this;

    self.$scope = $scope;

    $scope.isUniqueLin = function (value) {
      if (!value) {
        return true;
      }

      return Person.query({ lin: value, exact: true })
        .$promise
        .then(function (persons) {
          return persons.length === 0;
        });
    };
  }

  PersonDataCtrl.$inject = ['$scope', 'Person'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular, $));
