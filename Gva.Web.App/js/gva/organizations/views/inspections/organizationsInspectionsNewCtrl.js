/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspections,
    organizationInspection
  ) {
    $scope.organizationInspection = organizationInspection;


    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return OrganizationInspections
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
    'OrganizationInspections',
    'organizationInspection'
  ];

  OrganizationsInspectionsNewCtrl.$resolve = {
    organizationInspection: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              examiners: [],
              auditDetails: [],
              disparities: []
            },
            applications: [application]
          };
        }
        else {
          return {
            part: {
              examiners: [],
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
