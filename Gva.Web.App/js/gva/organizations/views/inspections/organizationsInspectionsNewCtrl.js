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
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
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
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              examiners: [{ sortOrder: 1 }],
              auditDetails: [],
              disparities: []
            },
            applications: [application]
          };
        }
        else {
          return {
            part: {
              examiners: [{ sortOrder: 1 }],
              auditDetails: [],
              disparities: []
            },
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsNewCtrl', OrganizationsInspectionsNewCtrl);
}(angular));
