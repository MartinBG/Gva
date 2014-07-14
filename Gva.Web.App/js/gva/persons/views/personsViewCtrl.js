/*global angular*/
(function (angular) {
  'use strict';

  function PersonsViewCtrl(
    $scope,
    $state,
    $stateParams,
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
      return $state.go('root.applications.edit.case', {
        id: appId,
        filter: $stateParams.filter
      });
    };
  }

  PersonsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'person',
    'application'
  ];

  PersonsViewCtrl.$resolve = {
    person: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.get($stateParams).$promise;
      }
    ],
    application: [
      '$stateParams',
      'PersonApplications',
      function ResolveApplication($stateParams, PersonApplications) {
        if (!!$stateParams.appId) {
          return PersonApplications.get($stateParams).$promise
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
