/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegViewCtrl($scope, $state, Aircrafts, scFormParams) {
    $scope.aircraftId = scFormParams.lotId;

    $scope.rereg = function (ind) {
      return Aircrafts.getNextActNumber({
        registerId: $scope.model.register.nomValueId
      }).$promise.then(function (result) {
        $scope.model.actNumber = result.actNumber;
        return $state.go('root.aircrafts.view.regsFM.new',
          { oldInd: ind },
          {},
          $scope.model);
      });
    };

    $scope.dereg = function (ind) {
      return $state.go('root.aircrafts.view.regsFM.dereg', {
        id: scFormParams.lotId,
        ind: ind
      });
    };
  }

  AircraftCertRegViewCtrl.$inject = ['$scope', '$state', 'Aircrafts', 'scFormParams'];

  angular.module('gva').controller('AircraftCertRegViewCtrl', AircraftCertRegViewCtrl);
}(angular));
