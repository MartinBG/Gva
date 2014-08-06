/*global angular*/
(function (angular) {
  'use strict';

  function ChoosePersonModalCtrl(
    $scope,
    $modalInstance,
    Persons,
    scModalParams,
    persons
  ) {
    $scope.persons = persons;

    $scope.filters = {
      lin: null,
      uin: scModalParams.uin,
      names: scModalParams.names
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
    'scModalParams',
    'persons'
  ];

  ChoosePersonModalCtrl.$resolve = {
    persons: [
      'Persons',
      'scModalParams',
      function (Persons, scModalParams) {
        return Persons.query(scModalParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChoosePersonModalCtrl', ChoosePersonModalCtrl);
}(angular));
