/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationApproval,
    approval,
    selectedLimitation
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
        return OrganizationApproval
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.organizations.view.approvals.search');
          });
      }
      else {
        return OrganizationApproval
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

            return OrganizationApproval
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
    'OrganizationApproval',
    'approval',
    'selectedLimitation'
  ];

  ApprovalsEditCtrl.$resolve = {
    approval: [
      '$stateParams',
      'OrganizationApproval',
      function ($stateParams, OrganizationApproval) {
        return OrganizationApproval.get($stateParams).$promise;
      }
    ],
    selectedLimitation: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApprovalsEditCtrl', ApprovalsEditCtrl);
}(angular, _));