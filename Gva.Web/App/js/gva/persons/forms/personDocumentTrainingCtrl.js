/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope, $stateParams, PersonDocumentTraining) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true : false);
    };

    var checkValue = function (param, value) {
      if (!value || !$stateParams.id) {
        return true;
      }

      var queryParams = { id: $stateParams.id };
      if (param === 'otypeid' || param === 'oroleid') {
        queryParams[param] = value.nomTypeValueId;
      } else {
        queryParams[param] = value;
      }

      return PersonDocumentTraining.query(queryParams).$promise
        .then(function (documentTrainings) {
          return documentTrainings.length === 0 ||
            (documentTrainings.length === 1 &&
            documentTrainings[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueValidDateFrom = function (value) {
      return checkValue('datef', value);
    };

    $scope.isUniqueDocNumber = function (value) {
      return checkValue('number', value);
    };

    $scope.isUniqueDocPublisher = function (value) {
      return checkValue('publ', value);
    };

    $scope.isUniqueOtherDocTypeId = function (value) {
      return checkValue('otypeid', value);
    };

    $scope.isUniqueOtherDocRoleId = function (value) {
      return checkValue('oroleid', value);
    };

    $scope.isUniqueDocPersonNumber = function (value) {
      return checkValue('pnumber', value);
    };
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentTraining'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
