/*global angular,_*/
(function (angular) {
  'use strict';

  function StaffManagementEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagements,
    organizationStaffManagement,
    scMessage
  ) {
    var originalStaffManagement = _.cloneDeep(organizationStaffManagement);

    $scope.organizationStaffManagement = organizationStaffManagement;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationStaffManagement = _.cloneDeep(originalStaffManagement);
    };

    $scope.save = function () {
      return $scope.editStaffManagement.$validate()
        .then(function () {
          if ($scope.editStaffManagement.$valid) {
            return OrganizationStaffManagements
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
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationStaffManagements
            .remove({ id: $stateParams.id, ind: organizationStaffManagement.partIndex })
            .$promise.then(function () {
              return $state.go('root.organizations.view.staffManagement.search');
            });
        }
      });
    };
  }

  StaffManagementEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffManagements',
    'organizationStaffManagement',
    'scMessage'
  ];

  StaffManagementEditCtrl.$resolve = {
    organizationStaffManagement: [
      '$stateParams',
      'OrganizationStaffManagements',
      function ($stateParams, OrganizationStaffManagements) {
        return OrganizationStaffManagements.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StaffManagementEditCtrl', StaffManagementEditCtrl);
}(angular));
