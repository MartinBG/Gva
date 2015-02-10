/*global angular*/

(function (angular) {
  'use strict';

  function AppExSystDataCtrl($scope, scModal) {
    $scope.addTests = function () {
      var modalInstance = scModal.open('appExSystChooseTests', {
        qualificationCode: $scope.model.qualificationCode,
        certCampCode: $scope.model.certCampaign.code,
        tests: $scope.model.tests
      });

    modalInstance.result.then(function (tests) {
        $scope.model.tests = tests;
      });

      return modalInstance.opened;
    };
  }

  AppExSystDataCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppExSystDataCtrl', AppExSystDataCtrl);
}(angular));
