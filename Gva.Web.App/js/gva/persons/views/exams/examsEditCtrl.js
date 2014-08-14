/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExamsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExams,
    exam,
    scMessage
  ) {
    var originalExam = _.cloneDeep(exam);

    $scope.exam = exam;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

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

    $scope.deleteExam = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonExams.remove({ id: $stateParams.id, ind: $stateParams.ind })
            .$promise.then(function () {
              return $state.go('root.persons.view.examASs.search');
            });
        }
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
    'PersonExams',
    'exam',
    'scMessage'
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
