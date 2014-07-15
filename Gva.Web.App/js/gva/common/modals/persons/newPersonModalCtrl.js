/*global angular*/
(function (angular) {
  'use strict';

  function NewPersonModalCtrl(
    $scope,
    $modalInstance,
    Persons,
    person
  ) {
    $scope.form = {};
    $scope.person = person;

    $scope.save = function () {
      return $scope.form.newPersonForm.$validate().then(function () {
        if ($scope.form.newPersonForm.$valid) {
          return Persons.save($scope.person).$promise.then(function (savedPerson) {
            return $modalInstance.close(savedPerson.id);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewPersonModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Persons',
    'person'
  ];

  angular.module('gva').controller('NewPersonModalCtrl', NewPersonModalCtrl);
}(angular));
