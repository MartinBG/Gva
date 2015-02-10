/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChoosePersonsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    persons
  ) {
    $scope.selectedPersons = [];

    $scope.persons = persons;

    $scope.addPersons = function () {
      return $modalInstance.close($scope.selectedPersons);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectPerson = function (event, person) {
      if ($(event.target).is(':checked')) {
        $scope.selectedPersons.push(person);
      }
      else {
        $scope.selectedPersons = _.without($scope.selectedPersons, person);
      }
    };
  }

  ChoosePersonsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'persons'
  ];


  ChoosePersonsModalCtrl.$resolve = {
    persons: [
      'scModalParams',
      'Persons',
      function (scModalParams, Persons) {
        return Persons.query().$promise
        .then(function (persons) {
          return _.filter(persons, function (person) {
            return _.where(scModalParams.includedPersons,
              { id: person.id })
              .length === 0;
          });
        });
      }
    ]
  };

  angular.module('gva').controller('ChoosePersonsModalCtrl', ChoosePersonsModalCtrl);
}(angular, _, $));
