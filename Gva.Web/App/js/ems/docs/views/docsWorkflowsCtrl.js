/*global angular*/
(function (angular) {
  'use strict';

  function DocsWorkflowsCtrl(
    $scope
  ) {
    $scope.removeDocWorkflow = function () {
    };
  }

  DocsWorkflowsCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocsWorkflowsCtrl', DocsWorkflowsCtrl);
}(angular));
