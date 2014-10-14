/*global angular*/
(function (angular) {
  'use strict';

  function CommonDocumentApplicationCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.setPart = scFormParams.setPart;
  }

  CommonDocumentApplicationCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva')
    .controller('CommonDocumentApplicationCtrl', CommonDocumentApplicationCtrl);
}(angular));
