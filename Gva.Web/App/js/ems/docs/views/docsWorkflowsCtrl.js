/*global angular*/
(function (angular) {
  'use strict';

  function DocsWorkflowsCtrl(
    $scope,
    DocWorkflow,
    doc
  ) {
    $scope.removeDocWorkflow = function (dwf) {
      return DocWorkflow.remove({ docId: doc.docId, docWorkflowId: dwf.docWorkflowId }).$promise
        .then(function (result) {
        doc.docWorkflows = result.docWorkflows;
      });
    };
  }

  DocsWorkflowsCtrl.$inject = [
    '$scope',
    'DocWorkflow',
    'doc'
  ];

  angular.module('ems').controller('DocsWorkflowsCtrl', DocsWorkflowsCtrl);
}(angular));
