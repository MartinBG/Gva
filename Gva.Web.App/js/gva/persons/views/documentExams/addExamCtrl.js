/*global angular*/
(function (angular) {
  'use strict';

  function AddExamCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentExam,
    exam
  ) {
    $scope.exam = exam;

    $scope.save = function () {
      return $scope.newExamForm.$validate()
        .then(function () {
          if ($scope.newExamForm.$valid) {
            return PersonDocumentExam
              .save({ id: $stateParams.id }, $scope.exam).$promise
              .then(function (savedExam) {
                return $state.go('^', {}, {}, {
                  selectedExams: [savedExam.partIndex]
                });
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AddExamCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentExam',
    'exam'
  ];

  AddExamCtrl.$resolve = {
    exam: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('AddExamCtrl', AddExamCtrl);
}(angular));
