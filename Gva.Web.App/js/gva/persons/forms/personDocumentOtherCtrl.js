/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentOtherCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  PersonDocumentOtherCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentOtherCtrl', PersonDocumentOtherCtrl);
}(angular));
