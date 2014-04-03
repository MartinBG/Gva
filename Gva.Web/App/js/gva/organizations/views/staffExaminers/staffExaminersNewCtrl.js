/*global angular*/
(function (angular) {
  'use strict';

  function StaffExaminersNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffExaminer,
    organizationStaffExaminer
  ) {
    $scope.organizationStaffExaminer = organizationStaffExaminer;

    $scope.save = function () {
      return $scope.organizationStaffExaminersForm.$validate()
        .then(function () {
          if ($scope.organizationStaffExaminersForm.$valid) {
            return OrganizationStaffExaminer
              .save({ id: $stateParams.id }, $scope.organizationStaffExaminer)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.staffExaminers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.staffExaminers.search');
    };
  }

  StaffExaminersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffExaminer',
    'organizationStaffExaminer'
  ];

  StaffExaminersNewCtrl.$resolve = {
    organizationStaffExaminer: function () {
      return {
        part: {},
        files: {
          hideApplications: false,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('StaffExaminersNewCtrl', StaffExaminersNewCtrl);
}(angular));
