/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseExamsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    exams
  ) {
    $scope.selectedExams = [];

    $scope.exams = _.filter(exams, function (exam) {
      return !_.contains(scModalParams.includedExams, exam.partIndex);
    });

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

  ChooseExamsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'exams'
  ];

  ChooseExamsModalCtrl.$resolve = {
    exams: [
      'PersonDocumentExams',
      'scModalParams',
      function (PersonDocumentExams, scModalParams) {
        return PersonDocumentExams.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseExamsModalCtrl', ChooseExamsModalCtrl);
}(angular, _, $));
