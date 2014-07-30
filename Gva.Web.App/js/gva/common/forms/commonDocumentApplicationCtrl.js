/*global angular*/
(function (angular) {
  'use strict';

  function CommonDocumentApplicationCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  CommonDocumentApplicationCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva')
    .controller('CommonDocumentApplicationCtrl', CommonDocumentApplicationCtrl);
}(angular));
