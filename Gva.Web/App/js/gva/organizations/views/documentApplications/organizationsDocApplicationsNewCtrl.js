﻿/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsDocApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentApplication,
    organizationDocumentApplication
  ) {

    $scope.organizationDocumentApplication = organizationDocumentApplication;

    $scope.save = function () {
      $scope.newDocumentApplicationForm.$validate()
         .then(function () {
            if ($scope.newDocumentApplicationForm.$valid) {
              return OrganizationDocumentApplication
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
    'OrganizationDocumentApplication',
    'organizationDocumentApplication'
  ];
  OrganizationsDocApplicationsNewCtrl.$resolve = {
    organizationDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva')
    .controller('OrganizationsDocApplicationsNewCtrl', OrganizationsDocApplicationsNewCtrl);
}(angular));
