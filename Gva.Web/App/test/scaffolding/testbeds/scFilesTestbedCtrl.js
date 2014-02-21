/*global angular*/
(function (angular) {
  'use strict';

  function ScFilesCtrl($scope, $timeout) {
    $scope.files = [];
    $scope.file = undefined;

    var multipleFiles = [
      { key: 'f111111111', relativePath: 'app/', name: 'file1' },
      { key: 'f222222222', relativePath: 'app/', name: 'file2' },
      { key: 'f333333333', relativePath: 'app/', name: 'file3' },
      { key: 'f444444444', relativePath: '', name: 'file4' }
    ];
    var singleFile = [
      { key: 'f111111111', relativePath: '', name: 'file1' }
    ];

    var file = { key: 'f111111111', relativePath: '', name: 'file1' };

    $scope.loadSingleFileWithDelay = function () {
      $scope.files = undefined;
      $timeout($scope.changeToSingleFile, 400);
    };

    $scope.changeToMultipleFiles = function () {
      $scope.files = multipleFiles;
    };

    $scope.changeToSingleFile = function () {
      $scope.files = singleFile;
      $scope.file = file;
    };

    $scope.changeToNoFiles = function () {
      $scope.files = [];
      $scope.file = undefined;
    };

  }

  ScFilesCtrl.$inject = ['$scope', '$timeout'];

  angular.module('scaffolding').controller('ScFilesTestbedCtrl', ScFilesCtrl);
}(angular));
