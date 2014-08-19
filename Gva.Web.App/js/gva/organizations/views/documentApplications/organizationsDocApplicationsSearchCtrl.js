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
