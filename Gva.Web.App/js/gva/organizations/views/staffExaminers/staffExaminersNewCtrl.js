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
      return $scope.newStaffExaminer.$validate()
        .then(function () {
          if ($scope.newStaffExaminer.$valid) {
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
      return { };
    }
  };

  angular.module('gva').controller('StaffExaminersNewCtrl', StaffExaminersNewCtrl);
}(angular));
