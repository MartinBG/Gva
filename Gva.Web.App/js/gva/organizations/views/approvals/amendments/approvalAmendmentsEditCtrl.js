/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalAmendmentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovalAmendments,
    approval,
    currentApprovalAmendment,
    approvalAmendments,
    scMessage
  ) {
    var originalApprovalAmendment = _.cloneDeep(currentApprovalAmendment);
    $scope.currentApprovalAmendment = currentApprovalAmendment;
    $scope.approvalAmendments = approvalAmendments;
    $scope.approval = approval;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.lastAmendmentIndex = _.last(approvalAmendments).partIndex;

    $scope.deleteCurrentAmendment= function () {
      $scope.approval.part.amendments.pop();

      if ($scope.approval.part.amendments.length === 0) {
        return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            return OrganizationApprovalAmendments
              .remove({ 
                id: $stateParams.id,
                ind: $stateParams.ind,
                index: $stateParams.index
              })
              .$promise.then(function () {

                $scope.approvalAmendments = _.remove($scope.approvalAmendments, function (le) {
                  return le.partIndex !== currentApprovalAmendment.partIndex;
                });

                if ($scope.approvalAmendments.length === 0) {
                  return $state.go('root.organizations.view.approvals.search');
                }
                else {
                  return $state.go(
                    'root.organizations.view.approvals.edit.amendments.edit',
                    { index: _.last($scope.approvalAmendments).partIndex });
                }
                return $state.go('root.organizations.view.approvals.search');
              });
          }
        });
      }
      else {
        return OrganizationApprovalAmendments
          .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.approval)
          .$promise.then(function () {
            originalApprovalAmendment = _.cloneDeep($scope.approval);
          });
      }
    };

    $scope.save = function () {
      return $scope.editApprovalAmendmentForm.$validate()
        .then(function () {
          if ($scope.editApprovalAmendmentForm.$valid) {
            $scope.editMode = 'saving';
            return OrganizationApprovalAmendments
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind,
                index: $stateParams.index,
                caseTypeId: $scope.caseTypeId
              }, $scope.currentApprovalAmendment)
              .$promise
              .then(function (amendment) {
                $scope.editMode = null;
                 var amendmentIndex = _.findIndex($scope.approvalAmendments, function (am) {
                  return am.partIndex === amendment.partIndex;
                });

                originalApprovalAmendment = _.cloneDeep($scope.currentApprovalAmendment);
                $scope.approvalAmendments[amendmentIndex] = _.cloneDeep(amendment);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.approval = _.cloneDeep(originalApprovalAmendment);
      $scope.editMode = null;
    };
  }

  ApprovalAmendmentsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApprovalAmendments',
    'approval',
    'currentApprovalAmendment',
    'approvalAmendments',
    'scMessage'
  ];

  ApprovalAmendmentsEditCtrl.$resolve = {
    currentApprovalAmendment: [
      '$stateParams',
      'OrganizationApprovalAmendments',
      function ($stateParams, OrganizationApprovalAmendments) {
        return OrganizationApprovalAmendments.get({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index
        }).$promise;
      }
    ],
    approvalAmendments: [
      '$stateParams',
      'OrganizationApprovalAmendments',
      function ($stateParams, OrganizationApprovalAmendments) {
        return OrganizationApprovalAmendments.query({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalAmendmentsEditCtrl', ApprovalAmendmentsEditCtrl);
}(angular, _));