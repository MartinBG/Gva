/*global angular*/
(function (angular) {
  'use strict';

  function ExamsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExamAnswers,
    PersonExam,
    exam
  ) {
    $scope.exam = exam;
    $scope.messages = {
      good: undefined,
      bad: undefined
    };

    $scope.save = function () {
      return $scope.newExamForm.$validate()
        .then(function () {
          if ($scope.newExamForm.$valid) {
            return PersonExam
              .save({ id: $stateParams.id }, $scope.exam).$promise
              .then(function () {
                return $state.go('root.persons.view.examASs.search');
              });
          }
        });
    };

    $scope.extractAnswers = function () {
      if ($scope.exam.files.length > 0 && $scope.exam.files[0].file !== null) {
        return PersonExamAnswers.get({
          id: $stateParams.id,
          fileKey: $scope.exam.files[0].file.key
        }).$promise
        .then(function (data) {
          if (data.msg) {
            $scope.messages.bad = data.msg;
            $scope.messages.good = undefined;
          }
          else {
            $scope.exam.part.commonQuestions1 = data.answ.commonQuestions1;
            $scope.exam.part.commonQuestions2 = data.answ.commonQuestions2;
            $scope.exam.part.specializedQuestions1 = data.answ.specializedQuestions1;
            $scope.exam.part.specializedQuestions2 = data.answ.specializedQuestions2;

            $scope.exam.part.file = 'data:image/jpg;base64,' + data.file;
            $scope.messages.good = 'Успешно';
            $scope.messages.bad = undefined;
          }
        });
      }
      else {
        $scope.messages.bad = 'Моля, прикачете файл!';
        $scope.messages.good = undefined;
      }
    };

    $scope.gradeExam = function () {
      
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.examASs.search');
    };
  }

  ExamsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonExamAnswers',
    'PersonExam',
    'exam'
  ];

  ExamsNewCtrl.$resolve = {
    exam: function () {
      return {
        part: {
          commonQuestions1: [
            {},
            {},
            {},
            {},
            {}
          ],
          commonQuestions2: [
            {},
            {},
            {},
            {},
            {}
          ],
          specializedQuestions1: [
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {}
          ],
          specializedQuestions2: [
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {}
          ]
        },
        files: []
      };
    }
  };

  angular.module('gva').controller('ExamsNewCtrl', ExamsNewCtrl);
}(angular));
