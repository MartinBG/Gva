/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEmploymentCtrl($scope, $stateParams, PersonDocumentEmployment) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };

    var checkValue = function (param, value) {
      if (!value || !$stateParams.id) {
        return true;
      }
      var queryParams = { id: $stateParams.id };
      if (param === 'hdate') {
        queryParams[param] = value;
      } else {
        queryParams[param] = value.nomTypeValueId;
      }
      return PersonDocumentEmployment.query(queryParams).$promise
        .then(function (employments) {
          return employments.length === 0 ||
            (employments.length === 1 &&
            employments[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueHireDate = function (value) {
      return checkValue('hdate', value);
    };

    $scope.isUniqueOrganizationId = function (value) {
      return checkValue('orgid', value);
    };
  }

  PersonDocumentEmploymentCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentEmployment'];

  angular.module('gva').controller('PersonDocumentEmploymentCtrl', PersonDocumentEmploymentCtrl);
}(angular));
