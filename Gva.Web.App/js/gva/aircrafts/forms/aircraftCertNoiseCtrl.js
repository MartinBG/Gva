/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertNoiseCtrl(
    $scope,
    Aircrafts,
    scFormParams
    ) {
    $scope.isUniqueFormNumber = function () {
       if ($scope.model.part.issueNumber) {
        return Aircrafts.isUniqueFormNumber({
          formNumber: $scope.model.part.issueNumber,
          lotId: scFormParams.lotId,
          partIndex: scFormParams.partIndex
        })
        .$promise
        .then(function (result) {
          return result.isUnique;
        });
      } else {
        return true;
       }
    };
  }

  AircraftCertNoiseCtrl.$inject = [
    '$scope',
    'Aircrafts',
    'scFormParams'
  ];

  angular.module('gva').controller('AircraftCertNoiseCtrl', AircraftCertNoiseCtrl);
}(angular));
