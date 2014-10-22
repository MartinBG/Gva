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
    $scope.lotId = $stateParams.id;
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
    aircraftCertMark: [
      '$stateParams',
      'AircraftCertMarks',
      function ($stateParams, AircraftCertMarks) {
        return AircraftCertMarks.newCertMark({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertMarksNewCtrl', CertMarksNewCtrl);
}(angular));
