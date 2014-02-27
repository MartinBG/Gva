/*global angular*/
(function (angular) {
  'use strict';

  function PersonsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Person,
    person
  ) {
    $scope.person = person;

    $scope.edit = function () {
      return $state.go('root.persons.view.edit');
    };
  }

  PersonsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Person',
    'person'
  ];

  PersonsViewCtrl.$resolve = {
    person: [
      '$stateParams',
      'Person',
      function ($stateParams, Person) {
        return Person.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonsViewCtrl', PersonsViewCtrl);
}(angular));
