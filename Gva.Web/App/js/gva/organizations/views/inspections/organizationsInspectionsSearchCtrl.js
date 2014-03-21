/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspection,
    organizationInspections) {

    $scope.organizationInspections = organizationInspections;

    $scope.newInspection = function () {
      return $state.go('root.organizations.view.inspections.new');
    };

    $scope.editInspection = function (inspection) {
      return $state.go('root.organizations.view.inspections.edit', {
        id: $stateParams.id,
        ind: inspection.partIndex
      });
    };

  }

  OrganizationsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationInspection',
    'organizationInspections'
  ];

  OrganizationsInspectionsSearchCtrl.$resolve = {
    organizationInspections: [
      '$stateParams',
      'OrganizationInspection',
      function ($stateParams, OrganizationInspection) {
        return OrganizationInspection.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsSearchCtrl', OrganizationsInspectionsSearchCtrl);
}(angular));
