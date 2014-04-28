/*global angular*/
(function (angular) {
  'use strict';

  function StaffManagementNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffManagement,
    organizationStaffManagement,
    selectedEmployment
  ) {
    $scope.organizationStaffManagement = organizationStaffManagement;

    var employment = selectedEmployment.pop();
    if(employment) {
      $scope.organizationStaffManagement.part.position = employment;
    } else {
      $scope.organizationStaffManagement.part.position = organizationStaffManagement.part ?
        organizationStaffManagement.part.position : '';
    }

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

    $scope.chooseEmployment = function () {
      return $state.go('.chooseEmployment');
    };
  }

  StaffManagementNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffManagement',
    'organizationStaffManagement',
    'selectedEmployment'
  ];

  StaffManagementNewCtrl.$resolve = {
    organizationStaffManagement: [
      'application',
      function (application) {
        if (application) {
          return {
            applications: [application]
          };
        }
        else {
          return {
            part: {}
          };
        }
      }
    ],
    selectedEmployment: function () {
      return [];
    }
  };

  angular.module('gva').controller('StaffManagementNewCtrl', StaffManagementNewCtrl);
}(angular));
