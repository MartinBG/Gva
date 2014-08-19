/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationStaffManagement
  ) {
    $scope.organizationStaffManagement = organizationStaffManagement;
  }

  StaffManagementSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationStaffManagement'
  ];

  StaffManagementSearchCtrl.$resolve = {
    organizationStaffManagement: [
      '$stateParams',
      'OrganizationStaffManagements',
      function ($stateParams, OrganizationStaffManagements) {
        return OrganizationStaffManagements.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('StaffManagementSearchCtrl', StaffManagementSearchCtrl);
}(angular));