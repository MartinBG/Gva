﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocumentExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentExams,
    exam
  ) {
    var originalExam = _.cloneDeep(exam);
    $scope.exam = exam;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editExamForm.$validate()
        .then(function () {
          if ($scope.editExamForm.$valid) {
            return PersonDocumentExams
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.exam)
              .$promise.then(function () {
                return $state.go('root.persons.view.exams.search');
              });
          }
        });
    };

    $scope.deleteExam = function () {
      return PersonDocumentExams
        .remove({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise.then(function () {
          return $state.go('root.persons.view.exams.search');
        });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.exam = _.cloneDeep(originalExam);
    };
  }

  DocumentExamsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentExams',
    'exam'
  ];

  DocumentExamsEditCtrl.$resolve = {
    exam: [
      '$stateParams',
      'PersonDocumentExams',
      function ($stateParams, PersonDocumentExams) {
        return PersonDocumentExams.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentExamsEditCtrl', DocumentExamsEditCtrl);
}(angular, _));
