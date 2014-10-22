/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovals,
    OrganizationApprovalAmendments,
    newApproval
  ) {
    $scope.newApproval = newApproval;
    $scope.newAmendment = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;

    $scope.$watch('newApproval.case.caseType', function () {
      if ($scope.newApproval['case'] && $scope.newApproval['case'].caseType) {
        OrganizationApprovalAmendments.newApprovalAmendment({
          id: $scope.lotId,
          appId: $scope.appId,
          caseTypeId: newApproval['case'].caseType.nomValueId
        }).$promise.then(function (amendment) {
          $scope.newAmendment = amendment;
        });
      }
    });

    $scope.save = function () {
      return $scope.newApprovalForm.$validate()
        .then(function () {
          if ($scope.newApprovalForm.$valid) {
            return OrganizationApprovals
              .save({ id: $stateParams.id }, {
                approval: $scope.newApproval,
                amendment: $scope.newAmendment
              }).$promise
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
    'OrganizationApprovalAmendments',
    'newApproval'
  ];

  ApprovalsNewCtrl.$resolve = {
    newApproval: [
      '$stateParams',
      'OrganizationApprovals',
      function ($stateParams, OrganizationApprovals) {
        return OrganizationApprovals.newApproval({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
