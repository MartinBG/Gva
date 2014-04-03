/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    organizationApproval,
    organizationAmendment
    ) {

    $scope.model = {
      approval : organizationApproval,
      organizationAmendment : organizationAmendment
    };

    $scope.save = function () {
      return $scope.organizationApprovalForm.$validate()
        .then(function () {
          if ($scope.organizationApprovalForm.$valid) {
            return OrganizationApproval
              .save({ id: $stateParams.id }, $scope.model).$promise
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
    'organizationApproval',
    'organizationAmendment'
  ];

  ApprovalsNewCtrl.$resolve = {
    organizationApproval: function () {
      return {
        part: {}
      };
    },
    organizationAmendment: function () {
      return {
        part: {
          includedDocuments: [],
          lims145: [],
          lims147: [],
          limsMG: []
        },
        files: {
          hideApplications: false,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('ApprovalsNewCtrl', ApprovalsNewCtrl);
}(angular));
