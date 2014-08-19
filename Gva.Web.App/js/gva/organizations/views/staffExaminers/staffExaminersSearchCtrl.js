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
