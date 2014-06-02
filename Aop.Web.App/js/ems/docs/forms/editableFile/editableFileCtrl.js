/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditableFileCtrl($scope) {
    $scope.isDisabled = true;

    if (typeof $scope.model.editableFiles === 'string') {
      $scope.model.editableFiles = JSON.parse($scope.model.editableFiles);
      $scope.model.editableFilesForComparison = _.cloneDeep($scope.model.editableFiles);
      $scope.model.editableFilesForComparison.push({emptyChoice: true});
    }
    
    $scope.model.editableFiles = _.isEmpty($scope.model.editableFiles)?
      [{}] : $scope.model.editableFiles;

    _.last($scope.model.editableFiles, function(lastEditableFile){
      var emptyChoice = _.last($scope.model.editableFilesForComparison);
      $scope.editableFiles = {
        currentEditableFile: lastEditableFile,
        lastEditableFile: lastEditableFile,
        editableFileForComparison: emptyChoice
      };
    });
    
    $scope.selectEditableFile = function (item) {
      $scope.editableFiles.currentEditableFile = item;
       $scope.editableFiles.editableFileForComparison =
         _.last($scope.model.editableFilesForComparison);
      $scope.isDisabled = $scope.readonly ||
        $scope.editableFiles.currentEditableFile !== $scope.editableFiles.lastEditableFile;
    };

    $scope.compareTo = function (item) {
      $scope.editableFiles.editableFileForComparison = item;
    };

    $scope.$watch('readonly', function(value){
      $scope.isDisabled = value || 
        $scope.editableFiles.currentEditableFile !== $scope.editableFiles.lastEditableFile;
      if(!value) {
        $scope.editableFiles.editableFileForComparison = 
          _.last($scope.model.editableFilesForComparison);
      }
    });

  }

  EditableFileCtrl.$inject = ['$scope'];

  angular.module('ems').controller('EditableFileCtrl', EditableFileCtrl);
}(angular, _));
