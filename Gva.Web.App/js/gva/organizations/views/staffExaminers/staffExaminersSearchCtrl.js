/*global angular*/
(function (angular) {
  'use strict';

  function StaffExaminersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationStaffExaminers
  ) {

    $scope.organizationStaffExaminers = organizationStaffExaminers;

    $scope.editStaffChecker = function (staffExaminer) {
      return $state.go('root.organizations.view.staffExaminers.edit', {
        id: $stateParams.id,
        ind: staffExaminer.partIndex
      });
    };

    $scope.newStaffExaminer = function () {
      return $state.go('root.organizations.view.staffExaminers.new');
    };
  }

  StaffExaminersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationStaffExaminers'
  ];

  StaffExaminersSearchCtrl.$resolve = {
    organizationStaffExaminers: [
      '$stateParams',
      'OrganizationStaffExaminers',
      function ($stateParams, OrganizationStaffExaminers) {
        return OrganizationStaffExaminers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('StaffExaminersSearchCtrl', StaffExaminersSearchCtrl);
}(angular));