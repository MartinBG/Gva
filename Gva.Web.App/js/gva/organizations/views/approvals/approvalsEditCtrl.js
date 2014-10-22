/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovals,
    approval
  ) {
    var originalApproval = _.cloneDeep(approval);
    $scope.approval = approval;
    $scope.lotId = $stateParams.id;

    $scope.newAmendment = function () {
      return $state.go('root.organizations.view.approvals.edit.amendments.new');
    };

    $scope.edit = function () {
      $scope.editApprovalMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editApprovalForm.$validate().then(function () {
        if ($scope.editApprovalForm.$valid) {
          return OrganizationApprovals
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            }, $scope.approval, $scope.caseTypeId)
            .$promise
            .then(function (savedApproval) {
              $scope.editApprovalMode = null;
              originalApproval = _.cloneDeep(savedApproval);
            });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editApprovalMode = null;
      $scope.approval = _.cloneDeep(originalApproval);
    };
  }

  ApprovalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApprovals',
    'approval'
  ];

  ApprovalsEditCtrl.$resolve = {
    approval: [
      '$stateParams',
      'OrganizationApprovals',
      function ($stateParams, OrganizationApprovals) {
        return OrganizationApprovals.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalsEditCtrl', ApprovalsEditCtrl);
}(angular, _));