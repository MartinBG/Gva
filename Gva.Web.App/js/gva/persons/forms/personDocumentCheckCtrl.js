/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.lotId = scFormParams.lotId;
    $scope.appId = scFormParams.appId;
    $scope.publisher = scFormParams.publisher;

    if(!$scope.isNew && !!scFormParams.report) {
      $scope.report = [scFormParams.report];
    }
  }

  PersonDocumentCheckCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
