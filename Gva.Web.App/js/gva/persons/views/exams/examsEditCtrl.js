/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExamAnswers,
    PersonExam,
    exam
  ) {
    var originalExam = _.cloneDeep(exam);
    $scope.exam = exam;
    $scope.editMode = null;
    $scope.messages = {
      good: undefined,
      bad: undefined
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editExamForm.$validate()
        .then(function () {
          if ($scope.editExamForm.$valid) {
            return PersonExam
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.exam)
              .$promise.then(function () {
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

    $scope.deleteExam = function () {
      return PersonExam
        .remove({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise.then(function () {
          return $state.go('root.persons.view.examASs.search');
        });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.exam = _.cloneDeep(originalExam);
    };
  }

  ExamsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonExamAnswers',
    'PersonExam',
    'exam'
  ];

  ExamsEditCtrl.$resolve = {
    exam: [
      '$stateParams',
      'PersonExam',
      function ($stateParams, PersonExam) {
        return PersonExam.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ExamsEditCtrl', ExamsEditCtrl);
}(angular, _));
