/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement) {
    $scope.organizationStaffManagement = organizationStaffManagement;

    $scope.save = function () {
      return $scope.organizationStaffManagementForm.$validate()
        .then(function () {
          if ($scope.organizationStaffManagementForm.$valid) {
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
      return {};
    }
  };

  angular.module('gva').controller('StaffManagementNewCtrl', StaffManagementNewCtrl);
}(angular));
