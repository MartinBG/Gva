﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function CertMarksEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertMarks,
    aircraftCertMark
  ) {
    var originalMark = _.cloneDeep(aircraftCertMark);

    $scope.isEdit = true;
    $scope.mark = aircraftCertMark;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.mark = _.cloneDeep(originalMark);
    };

    $scope.save = function () {
      return $scope.editCertMarkForm.$validate()
      .then(function () {
        if ($scope.editCertMarkForm.$valid) {
          return AircraftCertMarks
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.mark)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.marks.search');
            });
        }
      });
    };

    $scope.deleteMark = function () {
      return AircraftCertMarks.remove({ id: $stateParams.id, ind: aircraftCertMark.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.marks.search');
        });
    };
  }

  CertMarksEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertMarks',
    'aircraftCertMark'
  ];

  CertMarksEditCtrl.$resolve = {
    aircraftCertMark: [
      '$stateParams',
      'AircraftCertMarks',
      function ($stateParams, AircraftCertMarks) {
        return AircraftCertMarks.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertMarksEditCtrl', CertMarksEditCtrl);
}(angular));