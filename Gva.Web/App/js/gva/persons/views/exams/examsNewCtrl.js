/*global angular*/
(function (angular) {
  'use strict';

  function ExamsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExam,
    exam
  ) {
    $scope.exam = exam;

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

    $scope.cancel = function () {
      return $state.go('root.persons.view.examASs.search');
    };
  }

  ExamsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonExam',
    'exam'
  ];

  ExamsNewCtrl.$resolve = {
    exam: function () {
      return {
        part: {
          commonQuestions: [
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
          specializedQuestions: [
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
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
