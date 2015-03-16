/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseAppExamsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    exams
  ) {
    $scope.selectedExams = [];

    $scope.exams = exams;

    $scope.addExams = function () {
      return $modalInstance.close($scope.selectedExams);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectExam = function (event, exam) {
      if ($(event.target).is(':checked')) {
        $scope.selectedExams.push(exam);
      }
      else {
        $scope.selectedExams = _.without($scope.selectedExams, exam);
      }
    };
  }

  ChooseAppExamsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'exams'
  ];


  ChooseAppExamsModalCtrl.$resolve = {
    exams: [
      'scModalParams',
      'Applications',
      function (scModalParams, Applications) {
        return Applications.getExams().$promise
          .then(function (exams) {
          return _.filter(exams, function (exam) {
            return _.where(scModalParams.includedExams, {
              appExamId: exam.appExamId
            })
              .length === 0;
          });
        });
      }
    ]
  };

  angular.module('gva').controller('ChooseAppExamsModalCtrl', ChooseAppExamsModalCtrl);
}(angular, _, $));
