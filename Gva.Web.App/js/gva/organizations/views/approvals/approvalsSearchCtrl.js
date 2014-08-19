/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationApprovals
  ) {
    $scope.organizationApprovals = organizationApprovals;
  }

  ApprovalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationApprovals'
  ];

  ApprovalsSearchCtrl.$resolve = {
    organizationApprovals: [
      '$stateParams',
      'OrganizationApprovals',
      function ($stateParams, OrganizationApprovals) {
        return OrganizationApprovals.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('ApprovalsSearchCtrl', ApprovalsSearchCtrl);
}(angular));
