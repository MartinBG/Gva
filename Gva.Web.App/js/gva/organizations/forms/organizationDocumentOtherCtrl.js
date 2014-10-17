/*global angular*/
(function (angular) {
  'use strict';
  function OrgDocumentOtherCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  OrgDocumentOtherCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('OrgDocumentOtherCtrl', OrgDocumentOtherCtrl);
}(angular));
