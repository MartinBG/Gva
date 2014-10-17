/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentMedicalCtrl($scope, scFormParams) {
    $scope.personLin = scFormParams.personLin;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  PersonDocumentMedicalCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentMedicalCtrl', PersonDocumentMedicalCtrl);
}(angular));
