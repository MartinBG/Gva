/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessForm15Ctrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.$watch('model.inspector', function (inspectorModel) {
      if (!inspectorModel) {
        return;
      }

      if (inspectorModel.examiner) {
        $scope.inspectorType = 'examiner';
      } else if (inspectorModel.other) {
        $scope.inspectorType = 'other';
      }
    });
  }

  AircraftCertAirworthinessForm15Ctrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftCertAirworthinessForm15Ctrl',
    AircraftCertAirworthinessForm15Ctrl);
}(angular));
