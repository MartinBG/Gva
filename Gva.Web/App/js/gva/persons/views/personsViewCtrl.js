/*global angular*/
(function (angular) {
  'use strict';

  function PersonsViewCtrl($scope, $state, $stateParams, Person) {
    Person.get({ id: $stateParams.id }).$promise.then(function (person) {
      $scope.person = person;
    });

    $scope.edit = function () {
      return $state.go('persons.edit');
    };
  }

  PersonsViewCtrl.$inject = ['$scope', '$state', '$stateParams', 'Person'];

  angular.module('gva').controller('PersonsViewCtrl', PersonsViewCtrl);
}(angular));
