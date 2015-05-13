﻿/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
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
  }

  AircraftCertAirworthinessCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftCertAirworthinessCtrl',
    AircraftCertAirworthinessCtrl);
}(angular));
