/*global angular*/
(function (angular) {
  'use strict';

  function ExSystExamineesCtrl(
    $scope,
    examinees
  ) {
    $scope.examinees = examinees;
  }

  ExSystExamineesCtrl.$inject = [
    '$scope',
    'examinees'
  ];

  ExSystExamineesCtrl.$resolve = {
    examinees: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getExaminees().$promise;
      }
    ]
  };

  angular.module('gva').controller('ExSystExamineesCtrl', ExSystExamineesCtrl);
}(angular));