/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentApplication,
    organizationDocumentApplications
    ) {
    $scope.organizationDocumentApplications = organizationDocumentApplications;


    $scope.editApplication = function (application) {
      return $state.go('root.organizations.view.documentApplications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.newApplication = function () {
      return $state.go('root.organizations.view.documentApplications.new');
    };
  }

  OrganizationsDocApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentApplication',
    'organizationDocumentApplications'
  ];

  OrganizationsDocApplicationsSearchCtrl.$resolve = {
    organizationDocumentApplications: [
      '$stateParams',
      'OrganizationDocumentApplication',
      function ($stateParams, OrganizationDocumentApplication) {
        return OrganizationDocumentApplication.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsDocApplicationsSearchCtrl', OrganizationsDocApplicationsSearchCtrl);
}(angular));
