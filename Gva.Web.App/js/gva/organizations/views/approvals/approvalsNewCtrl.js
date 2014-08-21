/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovals,
    organizationApproval
  ) {
    $scope.approval = organizationApproval;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newApprovalForm.$validate()
        .then(function () {
          if ($scope.newApprovalForm.$valid) {
            return OrganizationApprovals
              .save({ id: $stateParams.id }, $scope.approval).$promise
              .then(function () {
                return $state.go('root.organizations.view.approvals.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.approvals.search');
    };
  }

  ApprovalsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApprovals',
    'organizationApproval'
  ];

  ApprovalsNewCtrl.$resolve = {
    organizationApproval: [
      '$stateParams',
      'OrganizationApprovals',
      function ($stateParams, OrganizationApprovals) {
        return OrganizationApprovals.newApproval({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
