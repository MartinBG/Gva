/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagements,
    organizationStaffManagement
  ) {
    $scope.organizationStaffManagement = organizationStaffManagement;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newStaffManagement.$validate()
        .then(function () {
          if ($scope.newStaffManagement.$valid) {
            return OrganizationStaffManagements
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
    'OrganizationStaffManagements',
    'organizationStaffManagement'
  ];

  StaffManagementNewCtrl.$resolve = {
    organizationStaffManagement: [
      '$stateParams',
      'OrganizationStaffManagements',
      function ($stateParams, OrganizationStaffManagements) {
        return OrganizationStaffManagements.newStaffManagement({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StaffManagementNewCtrl', StaffManagementNewCtrl);
}(angular));
