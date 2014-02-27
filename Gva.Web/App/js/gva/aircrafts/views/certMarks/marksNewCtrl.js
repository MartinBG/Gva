/*global angular*/
(function (angular) {
  'use strict';

  function CertMarksNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertMark,
    aircraftCertMark
  ) {
    $scope.isEdit = false;

    $scope.mark = aircraftCertMark;

    $scope.save = function () {
      $scope.aircraftCertMarkForm.$validate()
         .then(function () {
            if ($scope.aircraftCertMarkForm.$valid) {
              return AircraftCertMark
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
    'AircraftCertMark',
    'aircraftCertMark'
  ];
  CertMarksNewCtrl.$resolve = {
    aircraftCertMark: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertMarksNewCtrl', CertMarksNewCtrl);
}(angular));
