/*global angular*/
(function (angular) {
  'use strict';

  function ExSystTestsCtrl(
    $scope,
    tests
  ) {
    $scope.tests = tests;
  }

  ExSystTestsCtrl.$inject = [
    '$scope',
    'tests'
  ];

  ExSystTestsCtrl.$resolve = {
    tests: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getTests().$promise;
      }
    ]
  };

  angular.module('gva').controller('ExSystTestsCtrl', ExSystTestsCtrl);
}(angular));