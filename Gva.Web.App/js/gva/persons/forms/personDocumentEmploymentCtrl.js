/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEmploymentCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  PersonDocumentEmploymentCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentEmploymentCtrl', PersonDocumentEmploymentCtrl);
}(angular));
