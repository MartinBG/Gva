/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope, scFormParams) {
    $scope.personLin = scFormParams.personLin;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
