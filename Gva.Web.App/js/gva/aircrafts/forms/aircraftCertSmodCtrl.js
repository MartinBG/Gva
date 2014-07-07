/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertSmodCtrl($scope) {


    $scope.generateScode = function () {
      $scope.model.part.scode = '010001010100110000001001';
    };
  }

  AircraftCertSmodCtrl.$inject = ['$scope'];

  angular.module('gva').controller('AircraftCertSmodCtrl', AircraftCertSmodCtrl);
}(angular));
