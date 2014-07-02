/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'organizationDocumentApplications'
  ];

  OrganizationsDocApplicationsSearchCtrl.$resolve = {
    organizationDocumentApplications: [
      '$stateParams',
      'OrganizationDocumentApplications',
      function ($stateParams, OrganizationDocumentApplications) {
        return OrganizationDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsDocApplicationsSearchCtrl', OrganizationsDocApplicationsSearchCtrl);
}(angular));
