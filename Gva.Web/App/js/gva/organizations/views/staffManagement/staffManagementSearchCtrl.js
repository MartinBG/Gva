/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement
  ) {

    $scope.organizationStaffManagement = organizationStaffManagement;

    $scope.editStaffManagement = function (staffManagement) {
      return $state.go('root.organizations.view.staffManagement.edit', {
        id: $stateParams.id,
        ind: staffManagement.partIndex
      });
    };

    $scope.deleteStaffManagement = function (staffManagement) {
      return OrganizationStaffManagement
        .remove({ id: $stateParams.id, ind: staffManagement.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'OrganizationStaffManagement',
    'organizationStaffManagement'
  ];

  StaffManagementSearchCtrl.$resolve = {
    organizationStaffManagement: [
      '$stateParams',
      'OrganizationStaffManagement',
      function ($stateParams, OrganizationStaffManagement) {
        return OrganizationStaffManagement.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('StaffManagementSearchCtrl', StaffManagementSearchCtrl);
}(angular));