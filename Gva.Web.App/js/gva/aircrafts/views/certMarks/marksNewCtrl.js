/*global angular*/
(function (angular) {
  'use strict';

  function CertMarksNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertMarks,
    aircraftCertMark
  ) {
    $scope.isEdit = false;

    $scope.mark = aircraftCertMark;

    $scope.save = function () {
      return $scope.newCertMarkForm.$validate()
         .then(function () {
            if ($scope.newCertMarkForm.$valid) {
              return AircraftCertMarks
              .save({ id: $stateParams.id }, $scope.mark).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.marks.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.marks.search');
    };
  }

  CertMarksNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertMarks',
    'aircraftCertMark'
  ];
  CertMarksNewCtrl.$resolve = {
    aircraftCertMark: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertMarksNewCtrl', CertMarksNewCtrl);
}(angular));
