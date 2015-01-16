/*global angular, _*/
(function (angular, _) {
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
    $scope.tabs = {
      'persons.tabs.licences': 'root.persons.view.licences',
      'persons.tabs.qualifications': {
        'persons.tabs.ratings': 'root.persons.view.ratings',
        'persons.tabs.flyingExperiences': 'root.persons.view.flyingExperiences',
        'persons.tabs.documentTrainings': 'root.persons.view.documentTrainings',
        'persons.tabs.checks': 'root.persons.view.checks',
        'persons.tabs.documentLangCerts': 'root.persons.view.documentLangCerts',
        'persons.tabs.examASs': 'root.persons.view.examASs'
       },
      'persons.tabs.medicals': 'root.persons.view.medicals',
      'persons.tabs.personData': {
        'persons.tabs.addresses': 'root.persons.view.addresses',
        'persons.tabs.employments': 'root.persons.view.employments',
        'persons.tabs.documentEducations': 'root.persons.view.documentEducations',
        'persons.tabs.documentIds': 'root.persons.view.documentIds',
        'persons.tabs.statuses': 'root.persons.view.statuses'
      },
      'persons.tabs.docs': {
        'persons.tabs.others': 'root.persons.view.documentOthers'
      }
    };

    if(person.caseTypes.indexOf('staffExaminer') !== -1 ||
      person.caseTypes.indexOf('inspector') !== -1) {
      $scope.tabs = _.assign($scope.tabs, {'persons.tabs.reports': 'root.persons.view.reports'});
    } 

    $scope.tabs = _.assign($scope.tabs, {
      'persons.tabs.inventory': 'root.persons.view.inventory',
      'persons.tabs.applications': 'root.persons.view.documentApplications'
    });

    $scope.changeCaseType = function () {
      $stateParams.caseTypeId = $scope.caseType;
      $state.go($state.current, $stateParams, { reload: true });
    };

    $scope.edit = function () {
      return $state.go('root.persons.view.edit');
    };

    $scope.viewApplication = function (applicationId, partIndex) {
      return $state.go('root.applications.edit.data', {
        id: applicationId,
        set: $stateParams.set,
        lotId: $stateParams.id,
        ind: partIndex
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
}(angular, _));
