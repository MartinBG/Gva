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
    organizationApproval: function () {
      return {
        part: {
          amendments: [{
            includedDocuments: [],
            lims145: [],
            lims147: [],
            limsMG: []
          }]
        }
      };
    }
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
