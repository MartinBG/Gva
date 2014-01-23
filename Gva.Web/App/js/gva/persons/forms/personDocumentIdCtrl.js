﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentIdCtrl($scope, $stateParams, PersonDocumentId) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };

    var checkValue = function(param, value){
      if (!value || !$stateParams.id) {
        return true;
      }

      return PersonDocumentId.query({
        id: $stateParams.id,
        number: param === 'number'? value : undefined,
        typeid: param === 'typeid'? value.nomTypeValueId : undefined,
        publ: param === 'publ'? value : undefined,
        datef: param === 'datef'? value : undefined
      }).$promise
        .then(function (documentIds) {
          return documentIds.length === 0 ||
            (documentIds.length === 1  &&
            documentIds[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueDocNumber = function (value) {
      return checkValue('number', value);
    };

    $scope.isUniqueDocTypeId = function (value) {
      return checkValue('typeid', value);
    };

    $scope.isUniquePublisher = function (value) {
      return checkValue('publ', value);
    };

    $scope.isUniqueValidDateFrom = function (value) {
      return checkValue('datef', value);
    };
  }

  PersonDocumentIdCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentId'];

  angular.module('gva').controller('PersonDocumentIdCtrl', PersonDocumentIdCtrl);
}(angular));