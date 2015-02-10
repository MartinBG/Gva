/*global angular*/
(function (angular) {
  'use strict';

  function ExaminationSystemCtrl(
    $scope,
    ExaminationSystem
  ) {
    $scope.loadData = function () {
      return ExaminationSystem.loadData({loadExaminees : false}).$promise;
    };
    $scope.loadDataForExaminees = function () {
      return ExaminationSystem.loadData({loadExaminees : true}).$promise;
    };
    $scope.updateStates = function () {
      return ExaminationSystem.updateStates().$promise;
    };
  }

  ExaminationSystemCtrl.$inject = ['$scope', 'ExaminationSystem'];

  angular.module('gva').controller('ExaminationSystemCtrl', ExaminationSystemCtrl);
}(angular));
