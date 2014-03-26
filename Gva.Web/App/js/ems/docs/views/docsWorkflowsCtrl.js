/*global angular*/
(function (angular) {
  'use strict';

  function DocsWorkflowsCtrl(
    $scope,
    $state,
    $stateParams,
    DocWorkflow,
    doc
  ) {
    $scope.removeDocWorkflow = function (dwf) {
      return DocWorkflow.remove({
        docId: doc.docId,
        docVersion: doc.version,
        itemId: dwf.docWorkflowId
      })
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };
  }

  DocsWorkflowsCtrl.$inject = [
    '$scope',
    'DocWorkflow',
    '$state',
    '$stateParams',
    'doc'
  ];

  angular.module('ems').controller('DocsWorkflowsCtrl', DocsWorkflowsCtrl);
}(angular));
