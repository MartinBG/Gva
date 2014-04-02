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
    $scope.caseType = parseInt($stateParams.caseTypeId, 10);

    $scope.changeCaseType = function () {
      $stateParams.caseTypeId = $scope.caseType;
      $state.go($state.current, $stateParams, { reload: true });
    };

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
        return Person.get($stateParams).$promise.then(function (person) {
          /*jshint -W052*/
          person.age = ~~((Date.now() - new Date(person.birtDate)) / 31557600000);
          /*jshint +W052*/

          return person;
        });
      }
    ]
  };

  angular.module('gva').controller('PersonsViewCtrl', PersonsViewCtrl);
}(angular));
