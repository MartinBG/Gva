/*global angular,_*/
(function (angular) {
  'use strict';

  function StaffManagementEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement,
    selectedEmployment
  ) {
    var originalStaffManagement = _.cloneDeep(organizationStaffManagement);

    $scope.organizationStaffManagement = organizationStaffManagement;
    var employment = selectedEmployment.pop();
    if(employment) {
      $scope.organizationStaffManagement.part.position = employment;
    } else {
      $scope.organizationStaffManagement.part.position = organizationStaffManagement.part ?
        organizationStaffManagement.part.position : '';
    }
      
    $scope.editMode = null;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

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

    $scope.chooseEmployment = function () {
      return $state.go('.chooseEmployment');
    };
  }

  StaffManagementEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffManagement',
    'organizationStaffManagement',
    'selectedEmployment'
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
    ],
    selectedEmployment: function () {
      return [];
    }
  };

  angular.module('gva').controller('StaffManagementEditCtrl', StaffManagementEditCtrl);
}(angular));
