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

    $scope.editStaffManagement = function (staffManagement) {
      return $state.go('root.organizations.view.staffManagement.edit', {
        id: $stateParams.id,
        ind: staffManagement.partIndex
      });
    };

    $scope.newStaffManagement = function () {
      return $state.go('root.organizations.view.staffManagement.new');
    };
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