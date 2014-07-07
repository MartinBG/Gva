/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    PersonExams,
    SecurityExam,
    exam
  ) {
    var originalExam = _.cloneDeep(exam);
    $scope.exam = exam;
    $scope.editMode = null;
    $scope.messages = {};

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editExamForm.$validate()
        .then(function () {
          if ($scope.editExamForm.$valid) {
            return PersonExams.save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.exam)
              .$promise.then(function () {
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
          return SecurityExam.getAnswers({
            fileKey: $scope.exam.files[0].file.key,
            name: $scope.exam.files[0].file.name
          }, {}).$promise
          .then(function (data) {
            if (data.err) {
              $scope.messages.error = data.err;
              $scope.messages.success = undefined;
            }
            else {
              $scope.exam.part.commonQuestions = data.answ.commonQuestions1;
              data.answ.commonQuestions2.forEach(function (qs) {
                $scope.exam.part.commonQuestions.push(qs);
              });

              $scope.exam.part.specializedQuestions = data.answ.specializedQuestions1;
              data.answ.specializedQuestions2.forEach(function (qs) {
                $scope.exam.part.specializedQuestions.push(qs);
              });

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

    $scope.deleteExam = function () {
      return PersonExams.remove({ id: $stateParams.id, ind: $stateParams.ind })
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
    'l10n',
    'PersonExams',
    'SecurityExam',
    'exam'
  ];

  ExamsEditCtrl.$resolve = {
    exam: [
      '$stateParams',
      'PersonExams',
      function ($stateParams, PersonExams) {
        return PersonExams.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ExamsEditCtrl', ExamsEditCtrl);
}(angular, _));
