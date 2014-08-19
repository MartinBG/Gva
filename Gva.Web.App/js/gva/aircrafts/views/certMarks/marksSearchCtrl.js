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
