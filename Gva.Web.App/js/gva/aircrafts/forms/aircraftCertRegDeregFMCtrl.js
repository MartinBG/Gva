/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegDeregFMCtrl(
    $scope, 
    scFormParams,
    scModal,
    AircraftCertRegistrationsFM
    ) {
    $scope.partIndex = scFormParams.partIndex;
    $scope.lotId = scFormParams.lotId;

    $scope.getExportData = function () {
      AircraftCertRegistrationsFM.initExportData({
        id: $scope.lotId
      })
        .$promise
        .then(function (data) {
          $scope.model['export'] = data;
        });
    };

    $scope.print = function () {
      var params = {
        lotId: $scope.lotId,
        partIndex: $scope.partIndex
      };

      var modalInstance = scModal.open('printExportCert', params);

      return modalInstance.opened;
    };
  }

  AircraftCertRegDeregFMCtrl.$inject = [
    '$scope',
    'scFormParams',
    'scModal',
    'AircraftCertRegistrationsFM'
  ];

  angular.module('gva').controller('AircraftCertRegDeregFMCtrl', AircraftCertRegDeregFMCtrl);
}(angular));
