/*global angular*/
(function (angular) {
  'use strict';

  function CertMarksSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertMark,
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
    'AircraftCertMark',
    'marks'
  ];

  CertMarksSearchCtrl.$resolve = {
    marks: [
      '$stateParams',
      'AircraftCertMark',
      function ($stateParams, AircraftCertMark) {
        return AircraftCertMark.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertMarksSearchCtrl', CertMarksSearchCtrl);
}(angular));
