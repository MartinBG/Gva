/*global angular*/
(function (angular) {
  'use strict';

  function StaffExaminersNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffExaminers,
    organizationStaffExaminer
  ) {
    $scope.organizationStaffExaminer = organizationStaffExaminer;

    $scope.save = function () {
      return $scope.newStaffExaminer.$validate()
        .then(function () {
          if ($scope.newStaffExaminer.$valid) {
            return OrganizationStaffExaminers
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
    'OrganizationStaffExaminers',
    'organizationStaffExaminer'
  ];

  StaffExaminersNewCtrl.$resolve = {
    organizationStaffExaminer: [
      '$stateParams',
      'OrganizationStaffExaminers',
      function ($stateParams, OrganizationStaffExaminers) {
        return OrganizationStaffExaminers.newStaffExaminer({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StaffExaminersNewCtrl', StaffExaminersNewCtrl);
}(angular));
