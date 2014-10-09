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
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newInspectionForm
        .$validate()
        .then(function () {
          if ($scope.newInspectionForm.$valid) {
            return OrganizationInspections
              .save({ id: $stateParams.id }, $scope.organizationInspection)
              .$promise
              .then(function (inspection) {
                return $state.go('root.organizations.view.inspections.edit', { 
                  id: $stateParams.id,
                  ind: inspection.partIndex
                });
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
      '$stateParams',
      'OrganizationInspections',
      function ($stateParams, OrganizationInspections) {
        return OrganizationInspections.newInspection({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsNewCtrl', OrganizationsInspectionsNewCtrl);
}(angular));
