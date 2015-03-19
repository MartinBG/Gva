/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentMedicalCtrl($scope, scFormParams, Persons) {
    $scope.personLin = scFormParams.personLin;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.appId = scFormParams.appId;

    $scope.isUniqueDocData = function () {
      return Persons
        .isUniqueDocData({
          documentNumber: $scope.model.part.documentNumber,
          partIndex: $scope.model.partIndex,
          dateValidFrom: $scope.model.part.documentDateValidFrom,
          publisher: $scope.model.part.documentPublisher ? 
            $scope.model.part.documentPublisher.name : null
        })
      .$promise
      .then(function (result) {
        return result.isUnique;
      });
    };
  }

  PersonDocumentMedicalCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonDocumentMedicalCtrl', PersonDocumentMedicalCtrl);
}(angular));
