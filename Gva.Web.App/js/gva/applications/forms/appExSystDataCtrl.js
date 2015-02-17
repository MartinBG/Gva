/*global angular*/

(function (angular) {
  'use strict';

  function AppExSystDataCtrl($scope, scModal) {
    $scope.addExams = function () {
      var modalInstance = scModal.open('appExSystChooseExams', {
        qualificationCode: $scope.model.qualificationCode,
        certCampCode: $scope.model.certCampaign.code,
        exams: $scope.model.exams
      });

      modalInstance.result.then(function (exams) {
        $scope.model.exams = exams;
      });

      return modalInstance.opened;
    };
  }

  AppExSystDataCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppExSystDataCtrl', AppExSystDataCtrl);
}(angular));
