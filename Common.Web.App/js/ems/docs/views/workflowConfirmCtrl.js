/*global angular*/
(function (angular) {
  'use strict';

  function WorkflowConfirmCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    DocWorkflows,
    doc,
    workflowModel
  ) {
    $scope.workflowModel = workflowModel;

    switch ($state.current.url) {
    case '/signConfirm':
      $scope.workflowModel.docWorkflowActionAlias = 'Sign';
      $scope.workflowModel.confirmTitle = l10n.get('docs.edit.workflows.confirm.sign');
      break;
    case '/discussConfirm':
      $scope.workflowModel.docWorkflowActionAlias = 'Discuss';
      $scope.workflowModel.confirmTitle = l10n.get('docs.edit.workflows.confirm.discuss');
      break;
    case '/approvalConfirm':
      $scope.workflowModel.docWorkflowActionAlias = 'Approval';
      $scope.workflowModel.confirmTitle = l10n.get('docs.edit.workflows.confirm.approve');
      break;
    }

    $scope.save = function () {
      return $scope.workflowForm.$validate().then(function () {
        if ($scope.workflowForm.$valid) {
          return DocWorkflows
            .save({
              id: $scope.workflowModel.docId,
              docVersion: $scope.workflowModel.docVersion
            }, $scope.workflowModel)
            .$promise
            .then(function () {
              return $state.transitionTo($state.$current.parent, $stateParams, { reload: true });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  WorkflowConfirmCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
    'DocWorkflows',
    'doc',
    'workflowModel'
  ];

  WorkflowConfirmCtrl.$resolve = {
    workflowModel: ['doc',
      function (doc) {
        return {
          canSubstituteWorkflow: doc.flags.canSubstituteWorkflow,
          docId: doc.docId,
          docVersion: doc.version,
          unitUserId: doc.unitUser.unitUserId,
          principalUnitId: doc.unitUser.unitId,
          username: doc.unitUser.username,
          unitName: doc.unitUser.unitName
        };
      }
    ]
  };

  angular.module('ems').controller('WorkflowConfirmCtrl', WorkflowConfirmCtrl);
}(angular));
