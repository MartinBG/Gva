/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspection,
    organizationInspection
    ) {
    $scope.organizationInspection = organizationInspection;


    $scope.save = function () {
      return $scope.organizationInspectionForm.$validate()
      .then(function () {
        if ($scope.organizationInspectionForm.$valid) {
          return OrganizationInspection
            .save({ id: $stateParams.id }, $scope.organizationInspection)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.inspections.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.inspections.search');
    };
  }

  OrganizationsInspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationInspection',
    'organizationInspection'
  ];

  OrganizationsInspectionsNewCtrl.$resolve = {
    organizationInspection: [
      function () {
        return {
          part: {
            examiners: [{ sortOrder: 1 }],
            auditDetails: [],
            disparities: []
          },
          files: {
            hideApplications: false,
            files: []
          }
        };
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsNewCtrl', OrganizationsInspectionsNewCtrl);
}(angular));
