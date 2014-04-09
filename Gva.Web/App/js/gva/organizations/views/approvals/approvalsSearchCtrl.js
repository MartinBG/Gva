/*global angular*/
(function (angular) {
  'use strict';

  function ApprovalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    organizationApprovals
  ) {

    $scope.organizationApprovals = organizationApprovals;

    $scope.deleteApproval = function (approval) {
      return OrganizationApproval.remove({ id: $stateParams.id, ind: approval.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.viewAmendment = function (item) {
      return $state.go('root.organizations.view.amendments.search', {
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
    'OrganizationApproval',
    'organizationApprovals'
  ];

  ApprovalsSearchCtrl.$resolve = {
    organizationApprovals: [
      '$stateParams',
      'OrganizationApproval',
      function ($stateParams, OrganizationApproval) {
        return OrganizationApproval.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('ApprovalsSearchCtrl', ApprovalsSearchCtrl);
}(angular));