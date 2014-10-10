/*global angular*/
(function (angular) {
  'use strict';
  function OrgDocumentOtherCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  OrgDocumentOtherCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('OrgDocumentOtherCtrl', OrgDocumentOtherCtrl);
}(angular));
