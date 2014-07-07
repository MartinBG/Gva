/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SecurityExamBatchPartCtrl(
    $scope,
    $state,
    l10n,
    SecurityExam,
    PersonExam,
    pageModel,
    selectedPerson
  ) {
    if (!$scope.file) {
      return $state.go('root.persons.securityExam');
    }

    $scope.messages = {};
    $scope.exams = null;
    $scope.pageModel = pageModel;
    $scope.personExam = $scope.personExams[$scope.pageModel.currentPage - 1];
    if (!$scope.personExam.formReadonly) {
      $scope.personExam.part.examDate = $scope.commonData.examDate;
      $scope.personExam.part.commonQuestion = $scope.commonData.commonQuestion;
      $scope.personExam.part.specializedQuestion = $scope.commonData.specializedQuestion;
      $scope.personExam.part.inspector = $scope.commonData.inspector;
      $scope.personExam.files[0].caseType = $scope.commonData.caseType;
    }

    if (selectedPerson.length > 0) {
      $scope.personExam.lot.id = selectedPerson.pop();
    }

    $scope.$watch('personExam.lot.id', function (newValue, oldValue) {
      if ((newValue !== oldValue) || (newValue === oldValue)) {
        if (!!newValue) {
          return PersonExam.query({ id: $scope.personExam.lot.id }).$promise
            .then(function (data) {
              $scope.exams = _.first(_.sortBy(data, 'partIndex').reverse(), 2);
            });
        }
        else {
          $scope.exams = null;
        }

      }
    });
    $scope.$watch('personExam.part.examDate', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.examDate = newValue;
      }
    });
    $scope.$watch('personExam.part.commonQuestion', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.commonQuestion = newValue;
      }
    });
    $scope.$watch('personExam.part.specializedQuestion', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.specializedQuestion = newValue;
      }
    });
    $scope.$watch('personExam.part.inspector', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.inspector = newValue;
      }
    });
    $scope.$watch('personExam.files[0].caseType', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.caseType = newValue;
      }
    });

    $scope.extractAnswers = function () {
      var ext, exts = ['pdf', 'jpg', 'bmp', 'jpeg', 'png', 'tiff'];

      if ($scope.personExam.files.length > 0 && !!$scope.personExam.files[0].file) {
        ext = $scope.personExam.files[0].file.name.split('.').pop();
        if (exts.indexOf(ext) === -1) {
          $scope.messages.error = l10n.get('errorTexts.noPDForIMGFile');
          $scope.messages.success = undefined;
        }
        else {
          return SecurityExam.getAnswers({
            fileKey: $scope.personExam.files[0].file.key,
            name: $scope.personExam.files[0].file.name
          }, {}).$promise
          .then(function (data) {
            if (data.err) {
              $scope.messages.error = data.err;
              $scope.messages.success = undefined;
            }
            else {
              $scope.personExam.part.commonQuestions = data.answ.commonQuestions1;
              data.answ.commonQuestions2.forEach(function (qs) {
                $scope.personExam.part.commonQuestions.push(qs);
              });

              $scope.personExam.part.specializedQuestions = data.answ.specializedQuestions1;
              data.answ.specializedQuestions2.forEach(function (qs) {
                $scope.personExam.part.specializedQuestions.push(qs);
              });

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

    $scope.save = function () {
      return $scope.newExamBatchForm.$validate().then(function () {
        if ($scope.newExamBatchForm.$valid) {
          var exam = {
            part: $scope.personExam.part,
            files: $scope.personExam.files
          };

          return PersonExam.save({
            id: $scope.personExam.lot.id,
            ind: $scope.personExam.partIndex
          }, exam).$promise.then(function (data) {
            if ($scope.pageModel.currentPage + 1 > $scope.pageModel.pageCount) {
              return $state.go('root.persons.search');
            }

            return $scope.setCurrentPage($scope.pageModel.currentPage + 1).then(function () {
              $scope.personExam.formReadonly = true;
              if (!!data.partIndex) {
                $scope.personExam.partIndex = data.partIndex;
              }
            });
          });
        }
      });
    };

    $scope.edit = function () {
      $scope.personExam.formReadonly = false;
    };

    $scope.newPerson = function () {
      return $state.go('root.persons.securityExam.part.personNew');
    };

    $scope.selectPerson = function () {
      return $state.go('root.persons.securityExam.part.personSelect');
    };
  }

  SecurityExamBatchPartCtrl.$inject = [
    '$scope',
    '$state',
    'l10n',
    'SecurityExam',
    'PersonExam',
    'pageModel',
    'selectedPerson'
  ];

  SecurityExamBatchPartCtrl.$resolve = {
    selectedPerson: function () {
      return [];
    }
  };

  angular.module('gva').controller('SecurityExamBatchPartCtrl', SecurityExamBatchPartCtrl);
}(angular, _));
