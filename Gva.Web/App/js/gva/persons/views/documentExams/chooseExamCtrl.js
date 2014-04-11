/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseExamCtrl(
    $state,
    $stateParams,
    $scope,
    exams
  ) {
    if (!($state.payload && $state.payload.selectedExams)) {
      $state.go('^');
      return;
    }

    $scope.selectedExams = [];

    $scope.exams = _.filter(exams, function (exam) {
      return !_.contains($state.payload.selectedExams, exam.partIndex);
    });

    $scope.addExams = function () {
      return $state.go('^', {}, {}, {
        selectedExams: _.pluck($scope.selectedExams, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
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

  ChooseExamCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'exams'
  ];

  ChooseExamCtrl.$resolve = {
    exams: [
      '$stateParams',
      'PersonDocumentExam',
      function ($stateParams, PersonDocumentExam) {
        return PersonDocumentExam.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseExamCtrl', ChooseExamCtrl);
}(angular, _, $));
