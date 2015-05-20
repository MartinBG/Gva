/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessCtrl(
    $scope,
    Aircrafts,
    scFormParams) {
    $scope.lotId = scFormParams.lotId;
    $scope.partIndex = scFormParams.partIndex;

    $scope.$watch('model.inspector', function (inspectorModel) {
      if (!inspectorModel) {
        return;
      }

      if (inspectorModel.inspector) {
        $scope.inspectorType = 'inspector';
      } else if (inspectorModel.other) {
        $scope.inspectorType = 'other';
      }
    });

    $scope.$watch('model.airworthinessCertificateType', function (type) {
      if (type.alias === 'f25' || type.alias === 'f24') {
        Aircrafts.getNextFormNumber({
          formPrefix: type.alias === 'f25' ? 25 :  24
        })
        .$promise
        .then(function(result) {
          $scope.model.documentNumber = result.number;
        });
      } else {
        $scope.model.documentNumber = null;
      }
    });

    $scope.isUniqueFormNumber = function () {
       if ($scope.model.documentNumber) {
        return Aircrafts.isUniqueFormNumber({
          formNumber: $scope.model.documentNumber,
          lotId: $scope.lotId,
          partIndex: $scope.partIndex
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

  AircraftCertAirworthinessCtrl.$inject = [
    '$scope',
    'Aircrafts',
    'scFormParams'
  ];

  angular.module('gva').controller('AircraftCertAirworthinessCtrl',
    AircraftCertAirworthinessCtrl);
}(angular));
