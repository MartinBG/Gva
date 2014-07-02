/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApprovals,
    approval,
    selectedLimitation
  ) {
    var originalApproval = _.cloneDeep(approval);
    $scope.approval = approval;
    $scope.editMode = null;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

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
            $scope.backFromChild = false;
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
      $scope.backFromChild = false;
    };

    var limitation = selectedLimitation.pop();

    if(limitation) {
      var index = parseInt(limitation.index, 10),
        lastAmmendment = $scope.currentAmendment  || _.last(approval.part.amendments);

      if(limitation.limitationAlias === 'lim147limitations') {
        lastAmmendment.lims147[index].lim147limitation = limitation.name;
      } else if(limitation.limitationAlias === 'lim145limitations') {
        lastAmmendment.lims145[index].lim145limitation = limitation.name;
      } else if (limitation.limitationAlias === 'aircraftTypeGroups') {
        lastAmmendment.limsMG[index].aircraftTypeGroup = limitation.name;
      }
    }
  }

  ApprovalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationApprovals',
    'approval',
    'selectedLimitation'
  ];

  ApprovalsEditCtrl.$resolve = {
    approval: [
      '$stateParams',
      'OrganizationApprovals',
      function ($stateParams, OrganizationApprovals) {
        return OrganizationApprovals.get($stateParams).$promise;
      }
    ],
    selectedLimitation: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApprovalsEditCtrl', ApprovalsEditCtrl);
}(angular, _));