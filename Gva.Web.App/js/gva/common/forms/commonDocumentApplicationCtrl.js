/*global angular*/
(function (angular) {
  'use strict';

  function CommonDocumentApplicationCtrl($scope, scFormParams, Persons) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.setPart = scFormParams.setPart;
    $scope.hideCaseType = scFormParams.hideCaseType;

    $scope.isUniqueDocNumber = function () {
      if($scope.setPart === 'person') {
        return Persons
          .isUniqueDocNumber({
            documentNumber: $scope.model.part.documentNumber,
            partIndex: $scope.model.partIndex
          })
          .$promise
          .then(function (result) {
            return result.isUnique;
          });
      }
      else {
        return true;
      }
    };
  }

  CommonDocumentApplicationCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva')
    .controller('CommonDocumentApplicationCtrl', CommonDocumentApplicationCtrl);
}(angular));
