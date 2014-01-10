/*global angular*/
(function (angular) {
  'use strict';

  function DocWorkflowsCtrl(
    $scope
  ) {

    $scope.removeDocWorkflow = function () {

    };

  }

  DocWorkflowsCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocWorkflowsCtrl', DocWorkflowsCtrl);
}(angular));
