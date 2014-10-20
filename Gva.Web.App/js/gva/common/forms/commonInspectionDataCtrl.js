/*global angular*/
(function (angular) {
  'use strict';

  function CommonInspectionDataCtrl(
    $scope,
    scFormParams
  ) {
    $scope.setPart = scFormParams.setPart;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.appId = scFormParams.appId;
  }

  CommonInspectionDataCtrl.$inject = [
    '$scope',
    'scFormParams'
  ];

  angular.module('gva').controller('CommonInspectionDataCtrl', CommonInspectionDataCtrl);
}(angular));
