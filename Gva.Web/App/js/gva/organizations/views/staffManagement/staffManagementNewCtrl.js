/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement
  ) {
    $scope.organizationStaffManagement = organizationStaffManagement;

    $scope.save = function () {
      return $scope.newStaffManagement.$validate()
        .then(function () {
          if ($scope.newStaffManagement.$valid) {
            return OrganizationStaffManagement
              .save({ id: $stateParams.id }, $scope.organizationStaffManagement)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.staffManagement.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.staffManagement.search');
    };
  }

  StaffManagementNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffManagement',
    'organizationStaffManagement'
  ];

  StaffManagementNewCtrl.$resolve = {
    organizationStaffManagement: function () {
      return {
        part: {},
        files: {
          hideApplications: false,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('StaffManagementNewCtrl', StaffManagementNewCtrl);
}(angular));
