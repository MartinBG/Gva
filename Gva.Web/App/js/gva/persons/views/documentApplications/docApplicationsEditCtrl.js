/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplication,
    personDocumentApplication) {

    $scope.personDocumentApplication = personDocumentApplication;

    $scope.save = function () {
      $scope.editDocumentApplicationForm.$validate()
      .then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return PersonDocumentApplication
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentApplication)
            .$promise
            .then(function () {
              return $state.go('root.persons.view.documentApplications.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentApplications.search');
    };
  }

  DocApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentApplication',
    'personDocumentApplication'
  ];

  DocApplicationsEditCtrl.$resolve = {
    personDocumentApplication: [
      '$stateParams',
      'PersonDocumentApplication',
      function ($stateParams, PersonDocumentApplication) {
        return PersonDocumentApplication.get($stateParams).$promise
            .then(function (application) {
          application.files = {
            hideApplications: true,
            files: application.files
          };

          return application;
        });
      }
    ]
  };

  angular.module('gva').controller('DocApplicationsEditCtrl', DocApplicationsEditCtrl);
}(angular));