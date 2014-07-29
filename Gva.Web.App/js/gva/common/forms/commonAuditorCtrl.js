/*global angular*/
(function (angular) {
  'use strict';
  function CommonAuditorCtrl($scope, namedModal) {
    $scope.addExaminer = function () {
      var modalInstance = namedModal.open('chooseExaminers', {
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

  CommonAuditorCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva')
    .controller('CommonAuditorCtrl', CommonAuditorCtrl);
}(angular));