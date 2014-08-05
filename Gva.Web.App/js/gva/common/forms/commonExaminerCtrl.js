/*global angular*/
(function (angular) {
  'use strict';
  function CommonExaminerCtrl($scope, scModal) {
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

  CommonExaminerCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva')
    .controller('CommonExaminerCtrl', CommonExaminerCtrl);
}(angular));