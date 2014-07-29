/*global angular*/
(function (angular) {
  'use strict';

  function CommonDocumentApplicationsCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  CommonDocumentApplicationsCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva')
    .controller('CommonDocumentApplicationsCtrl', CommonDocumentApplicationsCtrl);
}(angular));
