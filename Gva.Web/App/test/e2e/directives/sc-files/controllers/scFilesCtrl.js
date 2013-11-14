/*global angular*/
(function (angular) {
  'use strict';

  function ScFilesCtrl($scope) {
    $scope.files = [];

    var multipleFiles = [
      { key: 'f111111111', relativePath: 'app/', name: 'file1' },
      { key: 'f222222222', relativePath: 'app/', name: 'file2' },
      { key: 'f333333333', relativePath: 'app/', name: 'file3' },
      { key: 'f444444444', relativePath: '', name: 'file4' }
    ];
    var singleFile = [
      { key: 'f111111111', relativePath: '', name: 'file1' }
    ];

    $scope.changeToMultipleFiles = function () {
      $scope.files = multipleFiles;
    };

    $scope.changeToSingleFile = function () {
      $scope.files = singleFile;
    };

    $scope.changeToNoFiles = function () {
      $scope.files = [];
    };
  }

  ScFilesCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScFilesCtrl', ScFilesCtrl);
}(angular));
