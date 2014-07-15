/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocMedicalCtrl($scope, $stateParams, Persons) {
    Persons.get({ id: $stateParams.id }).$promise.then(function (person) {
      $scope.person = person;
    });
  }

  PersonDocMedicalCtrl.$inject = ['$scope', '$stateParams', 'Persons'];

  angular.module('gva').controller('PersonDocMedicalCtrl', PersonDocMedicalCtrl);
}(angular));
