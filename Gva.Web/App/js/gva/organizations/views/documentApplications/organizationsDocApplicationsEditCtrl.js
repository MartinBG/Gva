/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentApplication,
    organizationDocumentApplication
  ) {
    var originalApplication = _.cloneDeep(organizationDocumentApplication);

    $scope.organizationDocumentApplication = organizationDocumentApplication;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationDocumentApplication.part = _.cloneDeep(originalApplication.part);
      $scope.$broadcast('cancel', originalApplication);
    };

    $scope.save = function () {
      $scope.editDocumentApplicationForm.$validate()
      .then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return OrganizationDocumentApplication
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
      return OrganizationDocumentApplication
        .remove({ id: $stateParams.id, ind: organizationDocumentApplication.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.documentApplications.search');
        });
    };
  }

  OrganizationsDocApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentApplication',
    'organizationDocumentApplication'
  ];

  OrganizationsDocApplicationsEditCtrl.$resolve = {
    organizationDocumentApplication: [
      '$stateParams',
      'OrganizationDocumentApplication',
      function ($stateParams, OrganizationDocumentApplication) {
        return OrganizationDocumentApplication.get($stateParams).$promise
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