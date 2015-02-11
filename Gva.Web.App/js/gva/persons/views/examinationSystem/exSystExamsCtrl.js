/*global angular*/
(function (angular) {
  'use strict';

  function ExSystExamsCtrl(
    $scope,
    exams
  ) {
    $scope.exams = exams;
  }

  ExSystExamsCtrl.$inject = [
    '$scope',
    'exams'
  ];

  ExSystExamsCtrl.$resolve = {
    exams: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getExams().$promise;
      }
    ]
  };

  angular.module('gva').controller('ExSystExamsCtrl', ExSystExamsCtrl);
}(angular));