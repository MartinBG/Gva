/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalAmendmentsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovals,
    OrganizationApprovalAmendments,
    approval,
    newApprovalAmendment
  ) {
    $scope.newApprovalAmendment = newApprovalAmendment;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;
    $scope.approval = approval;

    $scope.save = function () {
      return $scope.newApprovalAmendmentForm.$validate().then(function () {
        if ($scope.newApprovalAmendmentForm.$valid) {
          return OrganizationApprovalAmendments
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind
            }, $scope.newApprovalAmendment).$promise
            .then(function (edition) {
              return $state.go(
                'root.organizations.view.approvals.edit.amendments.edit',
                { index: edition.partIndex });
            });
        }
      });
    };

    $scope.cancel = function () {
      return OrganizationApprovals.lastEditionIndex($stateParams)
        .$promise
        .then(function (index) {
          return $state.go(
            'root.organizations.view.approvals.edit.amendments.edit',
            { index: index.lastIndex });
        });
    };
  }

  ApprovalAmendmentsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApprovals',
    'OrganizationApprovalAmendments',
    'approval',
    'newApprovalAmendment'
  ];

  ApprovalAmendmentsNewCtrl.$resolve = {
    newApprovalAmendment: [
      '$stateParams',
      'OrganizationApprovalAmendments',
      'approval',
      function ($stateParams, OrganizationApprovalAmendments, approval) {
        return OrganizationApprovalAmendments.newApprovalAmendment({
          id: $stateParams.id,
          approvalPartIndex: $stateParams.ind,
          appId: $stateParams.appId,
          caseTypeId: approval['case'].caseType.nomValueId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalAmendmentsNewCtrl', ApprovalAmendmentsNewCtrl);
}(angular));
