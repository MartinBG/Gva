/*global angular*/
(function (angular) {
  'use strict';

  function CertMarksEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertMark,
    aircraftCertMark
  ) {
    $scope.isEdit = true;

    $scope.mark = aircraftCertMark;

    $scope.save = function () {
      return $scope.aircraftCertMarkForm.$validate()
      .then(function () {
        if ($scope.aircraftCertMarkForm.$valid) {
          return AircraftCertMark
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.mark)
            .$promise
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

  CertMarksEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertMark',
    'aircraftCertMark'
  ];

  CertMarksEditCtrl.$resolve = {
    aircraftCertMark: [
      '$stateParams',
      'AircraftCertMark',
      function ($stateParams, AircraftCertMark) {
        return AircraftCertMark.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertMarksEditCtrl', CertMarksEditCtrl);
}(angular));