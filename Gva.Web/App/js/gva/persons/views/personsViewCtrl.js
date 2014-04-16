/*global angular*/
(function (angular) {
  'use strict';

  function PersonsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Person,
    person,
    application
  ) {
    $scope.person = person;
    $scope.application = application;
    $scope.caseType = parseInt($stateParams.caseTypeId, 10);

    $scope.changeCaseType = function () {
      $stateParams.caseTypeId = $scope.caseType;
      $state.go($state.current, $stateParams, { reload: true });
    };

    $scope.edit = function () {
      return $state.go('root.persons.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', { id: appId });
    };
  }

  PersonsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Person',
    'person',
    'application'
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
    ],
    application: [
      '$stateParams',
      'PersonApplication',
      function ResolveApplication($stateParams, PersonApplication) {
        if (!!$stateParams.appId) {
          return PersonApplication.get($stateParams).$promise
            .then(function (result) {
              if (result.applicationId) {
                return result;
              }

              return null;
            });
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('PersonsViewCtrl', PersonsViewCtrl);
}(angular));
