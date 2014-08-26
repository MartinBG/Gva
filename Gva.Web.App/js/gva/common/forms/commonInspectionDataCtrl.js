/*global angular*/
(function (angular) {
  'use strict';

  function CommonInspectionDataCtrl(
    $scope,
    scFormParams
  ) {
    $scope.setPart = scFormParams.setPart;
  }

  CommonInspectionDataCtrl.$inject = [
    '$scope',
    'scFormParams'
  ];

  angular.module('gva').controller('CommonInspectionDataCtrl', CommonInspectionDataCtrl);
}(angular));
