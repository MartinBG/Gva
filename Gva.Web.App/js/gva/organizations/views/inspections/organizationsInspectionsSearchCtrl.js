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
