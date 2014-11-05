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

    $scope.isDeclinedApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'declined';
      }

      return false;
    };

    $scope.isDoneApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'done';
      }

      return false;
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
        return OrganizationDocumentApplications.query({id: $stateParams.id}).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsDocApplicationsSearchCtrl', OrganizationsDocApplicationsSearchCtrl);
}(angular));
