/*global angular,_*/
(function (angular) {
  'use strict';

  function DocApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplication,
    personDocumentApplication) {
    var originalApplication = _.cloneDeep(personDocumentApplication);

    $scope.personDocumentApplication = personDocumentApplication;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentApplication.part = _.cloneDeep(originalApplication.part);
      $scope.$broadcast('cancel', originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return PersonDocumentApplication
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.personDocumentApplication)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentApplications.search');
              });
          }
        });
    };

    $scope.deleteApplication = function () {
      return PersonDocumentApplication.remove({
        id: $stateParams.id,
        ind: personDocumentApplication.partIndex
      }).$promise.then(function () {
        return $state.go('root.persons.view.documentApplications.search');
      });
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