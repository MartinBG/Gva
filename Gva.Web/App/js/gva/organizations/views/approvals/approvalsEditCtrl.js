/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    organizationApproval
  ) {
    var originalApproval = _.cloneDeep(organizationApproval);
    $scope.organizationApproval = organizationApproval;
    $scope.editMode = null;

    $scope.$watch('organizationApproval.part.amendments | last', function (lastAmendment) {
      $scope.currentAmendment = lastAmendment;
      $scope.lastAmendment = lastAmendment;
    });

    $scope.selectAmendment = function (item) {
      $scope.currentAmendment = item;
    };

    $scope.newAmendment = function () {
      $scope.organizationApproval.part.amendments.push({
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
      $scope.organizationApproval.part.amendments.pop();

      if ($scope.organizationApproval.part.amendments.length === 0) {
        return OrganizationApproval
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.organizations.view.approvals.search');
          });
      }
      else {
        return OrganizationApproval
          .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationApproval)
          .$promise.then(function () {
            originalApproval = _.cloneDeep($scope.organizationApproval);
          });
      }
    };

    $scope.save = function () {
      return $scope.editApprovalForm.$validate()
        .then(function () {
          if ($scope.editApprovalForm.$valid) {
            $scope.editMode = 'saving';

            return OrganizationApproval
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationApproval)
              .$promise
              .then(function () {
                $scope.editMode = null;
                originalApproval = _.cloneDeep($scope.organizationApproval);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.organizationApproval = _.cloneDeep(originalApproval);
      $scope.editMode = null;
    };
  }

  ApprovalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApproval',
    'organizationApproval'
  ];

  ApprovalsEditCtrl.$resolve = {
    organizationApproval: [
      '$stateParams',
      'OrganizationApproval',
      function ($stateParams, OrganizationApproval) {
        return OrganizationApproval.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApprovalsEditCtrl', ApprovalsEditCtrl);
}(angular, _));