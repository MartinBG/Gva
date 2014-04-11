/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocumentExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentExam,
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
            return PersonDocumentExam
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.exam)
              .$promise.then(function () {
                return $state.go('root.persons.view.exams.search');
              });
          }
        });
    };

    $scope.deleteExam = function () {
      return PersonDocumentExam
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
    'PersonDocumentExam',
    'exam'
  ];

  DocumentExamsEditCtrl.$resolve = {
    exam: [
      '$stateParams',
      'PersonDocumentExam',
      function ($stateParams, PersonDocumentExam) {
        return PersonDocumentExam.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentExamsEditCtrl', DocumentExamsEditCtrl);
}(angular, _));
