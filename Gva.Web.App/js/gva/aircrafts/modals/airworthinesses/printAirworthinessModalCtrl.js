/*global angular*/
(function (angular) {
  'use strict';

  function PrintAirworthinessModalCtrl(
    $scope,
    $modalInstance,
    AircraftCertAirworthinessesFM,
    scModalParams,
    airworthiness
  ) {
    $scope.form = {};
    $scope.model = {
      stampNumber: airworthiness.part.stampNumber,
      lotId: scModalParams.lotId,
      partIndex: scModalParams.partIndex,
      stampNumberReadonly: !!airworthiness.part.stampNumber
    };

    $scope.save = function () {
      return $scope.form.printAirworthinessForm.$validate().then(function () {
        if ($scope.form.printAirworthinessForm.$valid) {
          airworthiness.part.stampNumber = $scope.model.stampNumber;

          return AircraftCertAirworthinessesFM.save({
            id: scModalParams.lotId,
            ind: scModalParams.partIndex
          }, airworthiness).$promise.then(function (savedAirworthiness) {
            return $modalInstance.close(savedAirworthiness);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  PrintAirworthinessModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'AircraftCertAirworthinessesFM',
    'scModalParams',
    'airworthiness'
  ];

  PrintAirworthinessModalCtrl.$resolve = {
    airworthiness: [
      'scModalParams',
      'AircraftCertAirworthinessesFM',
      function (scModalParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.get({
          id: scModalParams.lotId,
          ind: scModalParams.partIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PrintAirworthinessModalCtrl', PrintAirworthinessModalCtrl);
}(angular));
