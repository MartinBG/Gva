/*global angular*/
(function (angular) {
  'use strict';

  function ChoosePersonModalCtrl(
    $scope,
    $modalInstance,
    Persons,
    persons,
    uin,
    names
  ) {
    $scope.persons = persons;

    $scope.filters = {
      lin: null,
      uin: uin,
      names: names
    };

    $scope.search = function () {
      return Persons.query({
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization
      }).$promise.then(function (persons) {
        $scope.persons = persons;
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectPerson = function (person) {
      return $modalInstance.close(person.id);
    };
  }

  ChoosePersonModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Persons',
    'persons',
    'uin',
    'names'
  ];

  angular.module('gva').controller('ChoosePersonModalCtrl', ChoosePersonModalCtrl);
}(angular));
