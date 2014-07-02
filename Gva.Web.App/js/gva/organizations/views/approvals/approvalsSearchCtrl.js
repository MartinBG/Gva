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

    $scope.viewAmendment = function (item) {
      return $state.go('root.organizations.view.approvals.edit', {
        id: $stateParams.id,
        ind: item.partIndex
      });
    };

    $scope.newApproval = function () {
      return $state.go('root.organizations.view.approvals.new');
    };
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