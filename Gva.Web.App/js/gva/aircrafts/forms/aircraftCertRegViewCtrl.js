/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegViewCtrl($scope, $state, scFormParams) {
    $scope.aircraftId = scFormParams.lotId;

    $scope.rereg = function (ind) {
      return $state.go('root.aircrafts.view.regsFM.newWizzard', { oldInd: ind });
    };

    $scope.dereg = function (ind) {
      return $state.go('root.aircrafts.view.regsFM.dereg', {
        id: scFormParams.lotId,
        ind: ind
      });
    };
  }

  AircraftCertRegViewCtrl.$inject = ['$scope', '$state', 'scFormParams'];

  angular.module('gva').controller('AircraftCertRegViewCtrl', AircraftCertRegViewCtrl);
}(angular));
