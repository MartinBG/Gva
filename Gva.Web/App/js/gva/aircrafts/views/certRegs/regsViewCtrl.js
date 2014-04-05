/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsViewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationCurrentFM,
    AircraftCertMark,
    aircraftCertRegistration,
    marks
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;
    $scope.marks = marks;

    $scope.switchReg = function (ind) {
      return $state.go($state.current,
        {
          id: $stateParams.id,
          ind: ind
        });
    };

    $scope.editCertMark = function (mark) {
      return $state.go('root.aircrafts.view.marks.edit', {
        id: $stateParams.id,
        ind: mark.partIndex
      });
    };

    $scope.deleteCertMark = function (mark) {
      return AircraftCertMark.remove({ id: $stateParams.id, ind: mark.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newCertMark = function () {
      return $state.go('root.aircrafts.view.marks.new');
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };

  }

  CertRegsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationCurrentFM',
    'AircraftCertMark',
    'aircraftCertRegistration',
    'marks'
  ];

  CertRegsViewCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationCurrentFM',
      function ($stateParams, AircraftCertRegistrationCurrentFM) {
        return AircraftCertRegistrationCurrentFM.get({
          id: $stateParams.id
        }).$promise;
      }
    ],
    marks: [
      '$stateParams',
      'AircraftCertMark',
      function ($stateParams, AircraftCertMark) {
        return AircraftCertMark.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsViewCtrl', CertRegsViewCtrl);
}(angular));