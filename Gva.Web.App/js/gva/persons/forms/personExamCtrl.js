/*global angular*/
(function (angular) {
  'use strict';

  function PersonExamCtrl(
    $scope,
    SecurityExam
    ) {
    $scope.gradeExam = function () {
      return $scope.form.$validate().then(function () {
        if ($scope.form.$valid) {
          return SecurityExam.calculateGrade($scope.model).$promise.then(function (res) {
            $scope.model.score = res.result;
            $scope.model.passed = res.passed;
          });
        }
      });
    };
  }

  PersonExamCtrl.$inject = [
    '$scope',
    'SecurityExam'
  ];

  angular.module('gva').controller('PersonExamCtrl', PersonExamCtrl);
}(angular));
