/*global angular*/
(function (angular) {
  'use strict';

  function PersonExamCtrl(
    $scope,
    Exam
    ) {
    $scope.gradeExam = function () {
      return $scope.form.$validate().then(function () {
        if ($scope.form.$valid) {
          return Exam.calculateGrade($scope.model).$promise.then(function (res) {
            $scope.model.score = res.result;
            $scope.model.passed = res.passed;
          });
        }
      });
    };
  }

  PersonExamCtrl.$inject = [
    '$scope',
    'Exam'
  ];

  angular.module('gva').controller('PersonExamCtrl', PersonExamCtrl);
}(angular));
