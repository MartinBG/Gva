/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentApplications,
    organizationDocumentApplication
  ) {
    $scope.organizationDocumentApplication = organizationDocumentApplication;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      $scope.newDocumentApplicationForm.$validate()
         .then(function () {
            if ($scope.newDocumentApplicationForm.$valid) {
              return OrganizationDocumentApplications
              .save({ id: $stateParams.id }, $scope.organizationDocumentApplication).$promise
              .then(function () {
                return $state.go('root.organizations.view.documentApplications.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.documentApplications.search');
    };
  }

  OrganizationsDocApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentApplications',
    'organizationDocumentApplication'
  ];
  OrganizationsDocApplicationsNewCtrl.$resolve = {
    organizationDocumentApplication: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva')
    .controller('OrganizationsDocApplicationsNewCtrl', OrganizationsDocApplicationsNewCtrl);
}(angular));
