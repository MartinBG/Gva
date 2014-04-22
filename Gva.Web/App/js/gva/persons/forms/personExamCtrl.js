/*global angular*/
(function (angular) {
  'use strict';

  function PersonExamCtrl($scope) {
    $scope.gradeExam = function () {
      $scope.model.score = 27;
      $scope.model.passed = {
        nomValueId: 1,
        name: 'Да'
      };
    };

  }

  PersonExamCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonExamCtrl', PersonExamCtrl);
}(angular));
