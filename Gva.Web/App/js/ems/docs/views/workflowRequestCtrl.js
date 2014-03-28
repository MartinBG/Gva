/*global angular*/
(function (angular) {
  'use strict';

  function WorkflowRequestCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    DocWorkflow,
    doc,
    workflowModel
  ) {
    $scope.workflowModel = workflowModel;
    switch ($state.current.url) {
    case '/signRequest':
      $scope.workflowModel.docWorkflowActionAlias = 'SignRequest';
      $scope.workflowModel.confirmTitle =
        l10n.get('docs.edit.workflows.request.toUnitSign');
      break;
    case '/discussRequest':
      $scope.workflowModel.docWorkflowActionAlias = 'DiscussRequest';
      $scope.workflowModel.confirmTitle =
        l10n.get('docs.edit.workflows.request.toUnitDiscuss');
      break;
    case '/approvalRequest':
      $scope.workflowModel.docWorkflowActionAlias = 'ApprovalRequest';
      $scope.workflowModel.confirmTitle =
        l10n.get('docs.edit.workflows.request.toUnitApproval');
      break;
    case '/registrationRequest':
      $scope.workflowModel.docWorkflowActionAlias = 'RegistrationRequest';
      $scope.workflowModel.confirmTitle =
        l10n.get('docs.edit.workflows.request.toUnitRegistration');
      break;
    }

    $scope.save = function () {
      $scope.workflowForm.$validate().then(function () {
        if ($scope.workflowForm.$valid) {
          return DocWorkflow.save({
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

  WorkflowRequestCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
    'DocWorkflow',
    'doc',
    'workflowModel'
  ];

  WorkflowRequestCtrl.$resolve = {
    workflowModel: ['doc',
      function (doc) {
        return {
          canChooseUnit: doc.canSubstituteManagement,
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

  angular.module('ems').controller('WorkflowRequestCtrl', WorkflowRequestCtrl);
}(angular));
