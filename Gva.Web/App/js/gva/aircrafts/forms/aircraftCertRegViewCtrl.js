/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegViewCtrl($scope, $state) {
    $scope.rereg = function () {
      return $state.go('root.aircrafts.view.regsFM.new');
    };
  }

  AircraftCertRegViewCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('AircraftCertRegViewCtrl', AircraftCertRegViewCtrl);
}(angular));
