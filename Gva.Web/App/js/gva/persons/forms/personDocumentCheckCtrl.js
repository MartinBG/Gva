/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, $stateParams, PersonDocumentCheck) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };

    var checkValue = function (param, value) {
      if (!value || !$stateParams.id || value.length === 0) {
        return true;
      }

      var queryParams = { id: $stateParams.id };
      if (param === 'typeid') {
        queryParams[param] = value.nomTypeValueId;
      } else {
        queryParams[param] = value;
      }

      return PersonDocumentCheck.query(queryParams).$promise
        .then(function (checks) {
          return checks.length === 0 ||
            (checks.length === 1 &&
            checks[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueDocNumber = function (value) {
      return checkValue('number', value);
    };
    $scope.isUniqueDocPersonNumber = function (value) {
      return checkValue('pnumber', value);
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

  PersonDocumentCheckCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentCheck'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
