/*global angular*/
(function (angular) {
  'use strict';

  function CertMarksSearchCtrl(
    $scope,
    $state,
    $stateParams,
    marks
  ) {
    $scope.marks = marks;


    $scope.editCertMark = function (mark) {
      return $state.go('root.aircrafts.view.marks.edit', {
        id: $stateParams.id,
        ind: mark.partIndex
      });
    };

    $scope.newCertMark = function () {
      return $state.go('root.aircrafts.view.marks.new');
    };
  }

  CertMarksSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'marks'
  ];

  CertMarksSearchCtrl.$resolve = {
    marks: [
      '$stateParams',
      'AircraftCertMarks',
      function ($stateParams, AircraftCertMarks) {
        return AircraftCertMarks.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertMarksSearchCtrl', CertMarksSearchCtrl);
}(angular));
