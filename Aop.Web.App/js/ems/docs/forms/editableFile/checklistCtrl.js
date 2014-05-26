/*global angular*/
(function (angular) {
  'use strict';

  function ChecklistCtrl(
    $scope,
    $state,
    Doc) {

    if (typeof $scope.model.editableFile === 'string') {
      $scope.model.editableFile = JSON.parse($scope.model.editableFile);
    }

    $scope.copy = function () {
      return Doc
        .createChecklist({
          id: $scope.model.docId,
          copy: true
        }, {})
        .$promise
        .then(function (result) {
          return $state.go('root.docs.edit.view', { id: result.docId });
        });
    };

    $scope.correct = function () {
      return Doc
        .createChecklist({
          id: $scope.model.docId,
          correct: true
        }, {})
        .$promise
        .then(function (result) {
          return $state.go('root.docs.edit.view', { id: result.docId });
        });
    };

    $scope.generatePosition = function () {
      return undefined;
    };

    $scope.generateReport = function () {
      return undefined;
    };
  }

  ChecklistCtrl.$inject = [
    '$scope',
    '$state',
    'Doc'
  ];

  angular.module('ems').controller('ChecklistCtrl', ChecklistCtrl);
}(angular));
