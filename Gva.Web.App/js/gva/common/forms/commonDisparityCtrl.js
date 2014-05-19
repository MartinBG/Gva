/*global angular*/
(function (angular) {
  'use strict';
  function CommonDisparityCtrl($scope) {

    $scope.deleteDisparity = function (disparity) {
      $scope.$parent.deleteDisparity(disparity);
    };

  }

  angular.module('gva').controller('CommonDisparityCtrl', CommonDisparityCtrl);
}(angular));
