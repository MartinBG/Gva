/*global angular*/
(function (angular) {
  'use strict';
  function AirportDataCtrl($scope) {

    $scope.deleteFrequency= function (frequency) {
      var index = $scope.model.frequencies.indexOf(frequency);
      $scope.model.frequencies.splice(index, 1);
    };

    $scope.addFrequency = function () {
      $scope.model.frequencies.push({});
    };

    $scope.deleteAid= function (aid) {
      var index = $scope.model.frequencies.indexOf(aid);
      $scope.model.radioNavigationAids.splice(index, 1);
    };

    $scope.addAid = function () {
      $scope.model.radioNavigationAids.push({});
    };
  }

  angular.module('gva').controller('AirportDataCtrl', AirportDataCtrl);
}(angular));
