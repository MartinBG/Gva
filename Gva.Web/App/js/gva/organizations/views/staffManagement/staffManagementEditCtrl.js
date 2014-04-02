/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement
  ) {
    $scope.organizationStaffManagement = organizationStaffManagement;

    $scope.save = function () {
      return $scope.organizationStaffManagementForm.$validate()
        .then(function () {
          if ($scope.organizationStaffManagementForm.$valid) {
            return OrganizationStaffManagement
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.organizationStaffManagement)
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

  StaffManagementEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffManagement',
    'organizationStaffManagement'
  ];

  StaffManagementEditCtrl.$resolve = {
    organizationStaffManagement: [
      '$stateParams',
      'OrganizationStaffManagement',
      function ($stateParams, OrganizationStaffManagement) {
        return OrganizationStaffManagement.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StaffManagementEditCtrl', StaffManagementEditCtrl);
}(angular));
