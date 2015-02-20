/*global angular*/
(function (angular) {
  'use strict';

  function ExaminationSystemCtrl(
    $scope,
    ExaminationSystem
  ) {
    $scope.loadData = function () {
      return ExaminationSystem.loadData().$promise;
    };

    $scope.updateStates = function () {
      return ExaminationSystem.updateStates().$promise;
    };

    $scope.checkConnection = function () {
      return ExaminationSystem.checkConnection()
        .$promise
        .then(function (result) {
          $scope.showCheckResult = true;
          $scope.isConnected = result.isConnected;
        });
    };
  }

  ExaminationSystemCtrl.$inject = ['$scope', 'ExaminationSystem'];

  angular.module('gva').controller('ExaminationSystemCtrl', ExaminationSystemCtrl);
}(angular));
