/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChecklistCtrl(
    $scope,
    $state,
    $parse,
    Doc) {
    $scope.isEmptyChoice = true;

    $scope.$watch('model.editableFileForComparison', function(){
      $scope.isEmptyChoice = $scope.model.editableFileForComparison.emptyChoice === true ||
        $scope.model.editableFileForComparison.version === $scope.model.currentEditableFile.version;
      $('input[type=checkbox]').removeClass('checklist-current-value-checkbox');
      $('input[type=checkbox]').removeClass('checklist-compared-value-checkbox');

      _.each($('input[type=checkbox]'), function(checkbox){
        var ngModelStr = checkbox.attributes['ng-model'].value,
          checkboxValueInCurrentFile = $parse(ngModelStr)($scope),
          checkboxValueInFileForComparison = 
          $parse(ngModelStr.replace('currentEditableFile', 'editableFileForComparison'))($scope);

        if(!$scope.isEmptyChoice && checkboxValueInCurrentFile === true){
          checkbox.classList.add('checklist-current-value-checkbox');
        } else if (!$scope.isEmptyChoice && checkboxValueInFileForComparison === true) {
          checkbox.classList.add('checklist-compared-value-checkbox');
        }
      });
    });

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
    '$parse',
    'Doc'
  ];

  angular.module('ems').controller('ChecklistCtrl', ChecklistCtrl);
}(angular, _, $));
