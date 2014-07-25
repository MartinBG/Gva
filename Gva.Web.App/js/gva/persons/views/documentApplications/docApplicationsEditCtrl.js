/*global angular,_*/
(function (angular) {
  'use strict';

  function DocApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplications,
    personDocumentApplication,
    scMessage) {
    var originalApplication = _.cloneDeep(personDocumentApplication);

    $scope.personDocumentApplication = personDocumentApplication;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentApplication = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return PersonDocumentApplications
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
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentApplications.remove({
            id: $stateParams.id,
            ind: personDocumentApplication.partIndex
          }).$promise.then(function () {
            return $state.go('root.persons.view.documentApplications.search', {
              appId: null
            }, { reload: true });
          });
        }
      });
    };
  }

  DocApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentApplications',
    'personDocumentApplication',
    'scMessage'
  ];

  DocApplicationsEditCtrl.$resolve = {
    personDocumentApplication: [
      '$stateParams',
      'PersonDocumentApplications',
      function ($stateParams, PersonDocumentApplications) {
        return PersonDocumentApplications.get($stateParams).$promise
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
