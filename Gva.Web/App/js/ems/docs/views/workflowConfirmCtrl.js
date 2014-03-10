/*global angular*/
(function (angular) {
  'use strict';

  function WorkflowConfirmCtrl(
    $scope,
    $state,
    DocWorkflow,
    doc,
    workflowModel
  ) {
    switch($state.current.url) {
    case '/signConfirm':
      workflowModel.docWorkflowActionId = 5;
      break;
    case '/discussConfirm':
      workflowModel.docWorkflowActionId = 6;
      break;
    case '/approvalConfirm':
      workflowModel.docWorkflowActionId = 7;
      break;
    }

    $scope.workflowModel = workflowModel;

    $scope.save = function () {
      $scope.workflowForm.$validate().then(function () {
        if ($scope.workflowForm.$valid) {

          var workflowData = {
            confirm:$scope.workflowModel.confirm.id,
            note: $scope.workflowModel.note,
            userId: $scope.workflowModel.userId,
            userName: $scope.workflowModel.userName,
            docWorkflowActionId: $scope.workflowModel.docWorkflowActionId
          };

          return DocWorkflow.add({ docId: workflowModel.docId }, workflowData).$promise
            .then(function (result) {
            doc.docWorkflows = result.docWorkflows;
            return $state.go('^');
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
    'DocWorkflow',
    'doc',
    'workflowModel'
  ];

  WorkflowConfirmCtrl.$resolve = {
    workflowModel: ['doc',
      function (doc) {
        return {
          docId: doc.docId,
          userId: 1,
          userName: 'Admin'
        };
      }
    ]
  };

  angular.module('ems').controller('WorkflowConfirmCtrl', WorkflowConfirmCtrl);
}(angular));
