/*global angular*/
(function (angular) {
  'use strict';

  function ExamsNewCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
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
      var ext, exts = ['pdf', 'jpg', 'bmp', 'jpeg', 'png', 'tiff'];

      if ($scope.exam.files.length > 0 && !!$scope.exam.files[0].file) {
        ext = $scope.exam.files[0].file.name.split('.').pop();
        if (exts.indexOf(ext) === -1) {
          $scope.messages.error = l10n.get('errorTexts.noPDForIMGFile');
          $scope.messages.success = undefined;
        }
        else {
          return PersonExamAnswers.get({
            id: $stateParams.id,
            fileKey: $scope.exam.files[0].file.key,
            name: $scope.exam.files[0].file.name
          }).$promise
          .then(function (data) {
            if (data.err) {
              $scope.messages.error = data.err;
              $scope.messages.success = undefined;
            }
            else {
              $scope.exam.part.commonQuestions1 = data.answ.commonQuestions1;
              $scope.exam.part.commonQuestions2 = data.answ.commonQuestions2;
              $scope.exam.part.specializedQuestions1 = data.answ.specializedQuestions1;
              $scope.exam.part.specializedQuestions2 = data.answ.specializedQuestions2;

              $scope.exam.part.file = 'data:image/jpg;base64,' + data.file;
              $scope.messages.success = l10n.get('successTexts.successExtract');
              $scope.messages.error = undefined;
            }
          });
        }
      }
      else {
        $scope.messages.error = l10n.get('errorTexts.noFile');
        $scope.messages.success = undefined;
      }
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.examASs.search');
    };
  }

  ExamsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
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
