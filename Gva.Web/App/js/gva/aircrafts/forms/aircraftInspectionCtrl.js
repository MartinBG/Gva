/*global angular, _*/
(function (angular, _) {
  'use strict';
  function AircraftInspectionCtrl($scope) {
    
    $scope.deleteExaminer = function removeExaminer(examiner) {
      var index = $scope.model.examiners.indexOf(examiner);
      $scope.model.examiners.splice(index, 1);
    };

    $scope.addExaminer = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.examiners, 'sortOrder'))) + 1;

      $scope.model.examiners.push({
        sortOrder: sortOder
      });
    };
  }

  angular.module('gva').controller('AircraftInspectionCtrl', AircraftInspectionCtrl);
}(angular, _));
