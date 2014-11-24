﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonCommonDocCtrl($scope, scModal, scFormParams, Persons) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.categoryAlias = scFormParams.categoryAlias;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.appId = scFormParams.appId;
    $scope.lotId = scFormParams.lotId;

    $scope.choosePublisher = function () {
      var modalInstance = scModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.part.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };

    $scope.isUniqueDocNumber = function () {
      if($scope.model.part.documentNumber) {
        return Persons
          .isUniqueDocNumber({
            documentNumber: $scope.model.part.documentNumber,
            documentPersonNumber: $scope.model.part.documentPersonNumber,
            partIndex: $scope.model.partIndex
          })
        .$promise
        .then(function (result) {
          return result.isUnique;
        });
      } else {
        return true;
      }
    };

  }

  PersonCommonDocCtrl.$inject = ['$scope', 'scModal', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonCommonDocCtrl', PersonCommonDocCtrl);
}(angular));
