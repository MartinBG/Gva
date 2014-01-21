﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonEmploymentCtrl($scope, $stateParams, PersonEmployment) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };

    var checkValue = function (param, value) {
      if (!value || !$stateParams.id) {
        return true;
      }
      var object = { id: $stateParams.id };
      if (param === 'hdate') {
        object[param] = value;
      } else {
        object[param] = value.nomTypeValueId;
      }
      return PersonEmployment.query(object).$promise
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

  PersonEmploymentCtrl.$inject = ['$scope', '$stateParams', 'PersonEmployment'];

  angular.module('gva').controller('PersonEmploymentCtrl', PersonEmploymentCtrl);
}(angular));
