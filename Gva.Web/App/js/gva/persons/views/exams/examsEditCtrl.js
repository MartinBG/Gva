/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExam,
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
            return PersonExam
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.exam)
              .$promise.then(function () {
                return $state.go('root.persons.view.examASs.search');
              });
          }
        });
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
