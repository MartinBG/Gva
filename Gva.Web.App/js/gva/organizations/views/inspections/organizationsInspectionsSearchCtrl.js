/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationInspections
  ) {

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
    'organizationInspections'
  ];

  OrganizationsInspectionsSearchCtrl.$resolve = {
    organizationInspections: [
      '$stateParams',
      'OrganizationInspections',
      function ($stateParams, OrganizationInspections) {
        return OrganizationInspections.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsSearchCtrl', OrganizationsInspectionsSearchCtrl);
}(angular));
