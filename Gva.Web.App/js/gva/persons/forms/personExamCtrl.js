/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonExamCtrl(
    $scope,
    SecurityExam,
    l10n,
    scFormParams
    ) {
    var currFirstFile = $scope.model.cases[0] ? $scope.model.cases[0].file || {} : {};

    $scope.file = null;
    $scope.loadingImage = false;
    $scope.imageError = null;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;

    $scope.gradeExam = function () {
      return $scope.form.$validate().then(function () {
        if ($scope.form.$valid) {
          return SecurityExam.calculateGrade($scope.model.part).$promise.then(function (res) {
            $scope.model.part.score = res.result;
            $scope.model.part.passed = res.passed;
          });
        }
      });
    };

    $scope.$watch('model.cases[0].file', function (files) {
      var firstFile = _.find(files, function (file) {
        return !file.isDeleted && file.file;
      });

      if (!firstFile) {
        $scope.file = null;
        $scope.imageError = null;
        return;
      }

      if (firstFile.file.key === currFirstFile.key &&
          firstFile.file.name === currFirstFile.name &&
          $scope.file) {
        return;
      }

      $scope.loadingImage = true;
      $scope.imageError = null;
      var shouldExtractAnswers = firstFile.file.key !== currFirstFile.key ||
                                 firstFile.file.name !== currFirstFile.name;
      currFirstFile.key = firstFile.file.key;
      currFirstFile.name = firstFile.file.name;

      if (shouldExtractAnswers) {
        SecurityExam.getAnswers({
          fileKey: firstFile.file.key,
          name: firstFile.file.name
        }).$promise.then(function (data) {
          $scope.loadingImage = false;

          if (data.err) {
            $scope.imageError = l10n.get('errorTexts.' + data.err);
            $scope.file = null;
          }
          else {
            $scope.model.part.commonQuestions = data.answ.commonQuestions1;
            data.answ.commonQuestions2.forEach(function (qs) {
              $scope.model.part.commonQuestions.push(qs);
            });

            $scope.model.part.specializedQuestions = data.answ.specializedQuestions1;
            data.answ.specializedQuestions2.forEach(function (qs) {
              $scope.model.part.specializedQuestions.push(qs);
            });

            $scope.file = 'data:image/jpg;base64,' + data.file;
          }
        });
      }
      else {
        SecurityExam.getImage({
          fileKey: firstFile.file.key,
          name: firstFile.file.name
        }).$promise.then(function (data) {
          $scope.loadingImage = false;

          if (data.err) {
            $scope.file = null;
            $scope.imageError = l10n.get('errorTexts.' + data.err);
          }
          else {
            $scope.file = 'data:image/jpg;base64,' + data.image;
          }
        });
      }
    }, true);
  }

  PersonExamCtrl.$inject = [
    '$scope',
    'SecurityExam',
    'l10n',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonExamCtrl', PersonExamCtrl);
}(angular, _));
