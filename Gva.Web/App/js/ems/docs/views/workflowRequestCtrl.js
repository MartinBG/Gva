/*global angular*/
(function (angular) {
  'use strict';

  function WorkflowRequestCtrl(
    $scope,
    $state,
    DocWorkflow,
    doc,
    workflowModel
  ) {

    switch($state.current.url) {
    case '/signRequest':
      workflowModel.docWorkflowActionId = 1;
      break;
    case '/discussRequest':
      workflowModel.docWorkflowActionId = 2;
      break;
    case '/approvalRequest':
      workflowModel.docWorkflowActionId = 3;
      break;
    case '/registrationRequest':
      workflowModel.docWorkflowActionId = 4;
      break;
    }

    $scope.workflowModel = workflowModel;

    $scope.save = function () {
      $scope.workflowForm.$validate().then(function () {
        if ($scope.workflowForm.$valid) {

          var workflowData = {
            toUnits: $scope.workflowModel.toUnits,
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

  WorkflowRequestCtrl.$inject = [
    '$scope',
    '$state',
    'DocWorkflow',
    'doc',
    'workflowModel'
  ];

  WorkflowRequestCtrl.$resolve = {
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

  angular.module('ems').controller('WorkflowRequestCtrl', WorkflowRequestCtrl);
}(angular));
