/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegViewCtrl($scope, $state, $stateParams) {
    $scope.rereg = function () {
      return $state.go('root.aircrafts.view.regsFM.new');
    };

    $scope.dereg = function (ind) {
      return $state.go('root.aircrafts.view.regsFM.dereg', {
        id: $stateParams.id,
        ind: ind
      });
    };
  }

  AircraftCertRegViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams'
  ];

  angular.module('gva').controller('AircraftCertRegViewCtrl', AircraftCertRegViewCtrl);
}(angular));
