/*global angular*/
(function (angular) {
  'use strict';
  function CommonExaminersCtrl($scope, scModal, scFormParams) {
    $scope.addExaminerText = scFormParams.addExaminerText;
    $scope.examinerText = scFormParams.examinerText;
    $scope.examinersText = scFormParams.examinersText;
    $scope.noAvailableExaminersText = scFormParams.noAvailableExaminersText;

    $scope.addExaminer = function () {
      var modalInstance = scModal.open('chooseExaminers', {
        includedExaminers: $scope.model
      });

      modalInstance.result.then(function (selectedExaminers) {
        $scope.model.splice.apply($scope.model,
          [$scope.model.length, 0].concat(selectedExaminers));
      });

      return modalInstance.opened;
    };

    $scope.deleteExaminer = function removeExaminer(examiner) {
      var index = $scope.model.indexOf(examiner);
      $scope.model.splice.apply($scope.model, [index, 1]);
    };
  }

  CommonExaminersCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva')
    .controller('CommonExaminersCtrl', CommonExaminersCtrl);
}(angular));