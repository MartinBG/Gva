/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentApplications,
    organizationDocumentApplication,
    scMessage
  ) {
    var originalApplication = _.cloneDeep(organizationDocumentApplication);

    $scope.organizationDocumentApplication = organizationDocumentApplication;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationDocumentApplication = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      $scope.editDocumentApplicationForm.$validate()
      .then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return OrganizationDocumentApplications
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationDocumentApplication)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.documentApplications.search');
            });
        }
      });
    };

    $scope.deleteApplication = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationDocumentApplications
            .remove({ id: $stateParams.id, ind: organizationDocumentApplication.partIndex })
            .$promise.then(function () {
              return $state.go('root.organizations.view.documentApplications.search', {
                appId: null
              }, { reload: true });
            });
        }
      });
    };
  }

  OrganizationsDocApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentApplications',
    'organizationDocumentApplication',
    'scMessage'
  ];

  OrganizationsDocApplicationsEditCtrl.$resolve = {
    organizationDocumentApplication: [
      '$stateParams',
      'OrganizationDocumentApplications',
      function ($stateParams, OrganizationDocumentApplications) {
        return OrganizationDocumentApplications.get($stateParams).$promise
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

  angular.module('gva')
    .controller('OrganizationsDocApplicationsEditCtrl', OrganizationsDocApplicationsEditCtrl);
}(angular));
