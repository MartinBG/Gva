/*global angular*/
(function (angular) {
  'use strict';

  function DocsWorkflowsCtrl(
    $scope,
    $state,
    $stateParams,
    DocWorkflows,
    doc
  ) {
    $scope.doc = doc;
    $scope.canDeleteWorkflow = doc.flags.canDeleteWorkflow;

    $scope.removeDocWorkflow = function (dwf) {
      return DocWorkflows.remove({
        id: doc.docId,
        docVersion: doc.version,
        itemId: dwf.docWorkflowId
      }, {})
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };
  }

  DocsWorkflowsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocWorkflows',
    'doc'
  ];

  angular.module('ems').controller('DocsWorkflowsCtrl', DocsWorkflowsCtrl);
}(angular));
