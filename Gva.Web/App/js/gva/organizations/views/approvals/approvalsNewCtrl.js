/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    organizationApproval) {
    $scope.organizationApproval = organizationApproval;

    $scope.save = function () {
      return $scope.organizationApprovalForm.$validate()
        .then(function () {
          if ($scope.organizationApprovalForm.$valid) {
            return OrganizationApproval
              .save({ id: $stateParams.id }, $scope.organizationApproval).$promise
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
    'OrganizationApproval',
    'organizationApproval'
  ];

  ApprovalsNewCtrl.$resolve = {
    organizationApproval: function () {
      return {
        approval: {
          part: {}
        },
        amendment: {
          part: {
            includedDocuments: [],
            lims145: [],
            lims147: [],
            limsMG: []
          }
        }
      };
    }
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
