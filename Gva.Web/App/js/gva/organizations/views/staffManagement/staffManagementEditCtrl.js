/*global angular,_*/
(function (angular) {
  'use strict';

  function StaffManagementEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement
  ) {
    var originalStaffManagement = _.cloneDeep(organizationStaffManagement);

    $scope.organizationStaffManagement = organizationStaffManagement;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationStaffManagement.part = _.cloneDeep(originalStaffManagement.part);
      $scope.$broadcast('cancel', originalStaffManagement);
    };

    $scope.save = function () {
      return $scope.editStaffManagement.$validate()
        .then(function () {
          if ($scope.editStaffManagement.$valid) {
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

    $scope.deleteStaffManagement = function () {
      return OrganizationStaffManagement
        .remove({ id: $stateParams.id, ind: organizationStaffManagement.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.staffManagement.search');
        });
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
