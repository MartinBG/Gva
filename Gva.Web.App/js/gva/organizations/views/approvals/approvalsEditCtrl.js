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
    $scope.editMode = null;

    $scope.$watch('approval.part.amendments | last', function (lastAmendment) {
      $scope.currentAmendment = lastAmendment;
      $scope.lastAmendment = lastAmendment;
    });

    $scope.selectAmendment = function (item) {
      $scope.currentAmendment = item;
    };

    $scope.newAmendment = function () {
      $scope.approval.part.amendments.push({
          includedDocuments: [],
          lims145: [],
          lims147: [],
          limsMG: []
        });

      $scope.editMode = 'edit';
    };

    $scope.editLastAmendment = function () {
      $scope.editMode = 'edit';
    };

    $scope.deleteLastAmendment = function () {
      $scope.approval.part.amendments.pop();

      if ($scope.approval.part.amendments.length === 0) {
        return OrganizationApprovals
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.organizations.view.approvals.search');
          });
      }
      else {
        return OrganizationApprovals
          .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.approval)
          .$promise.then(function () {
            originalApproval = _.cloneDeep($scope.approval);
          });
      }
    };

    $scope.save = function () {
      return $scope.editApprovalForm.$validate()
        .then(function () {
          if ($scope.editApprovalForm.$valid) {
            $scope.editMode = 'saving';
            return OrganizationApprovals
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.approval)
              .$promise
              .then(function () {
                $scope.editMode = null;
                originalApproval = _.cloneDeep($scope.approval);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.approval = _.cloneDeep(originalApproval);
      $scope.editMode = null;
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
        return OrganizationApprovals.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalsEditCtrl', ApprovalsEditCtrl);
}(angular, _));