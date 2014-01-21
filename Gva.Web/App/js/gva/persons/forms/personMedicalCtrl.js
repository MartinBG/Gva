/*global angular, require*/
(function (angular) {
  'use strict';

  function PersonMedicalCtrl($scope, $stateParams, PersonMedical) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };
    var nomenclatures = require('./nomenclatures.sample');
    $scope.limitationsTypes = nomenclatures.medicalLimitationTypes.map(function (limitation) {
      limitation.text = limitation.name;
      limitation.id = limitation.nomTypeValueId;
      return limitation;
    });

    var checkValue = function (param, value) {
      if (!value) {
        return true;
      }
      var object = { id: $stateParams.id };
      if (param === 'medclid') {
        object.medclid = value.nomTypeValueId;
      } else {
        object[param] = value;
      }

      return PersonMedical.query(object).$promise
        .then(function (medicals) {
          return medicals.length === 0 ||
            (medicals.length === 1 &&
            medicals[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueDocNumber = function (value) {
      return checkValue('num', value);
    };

    $scope.isUniqueDocNumberSuffix = function (value) {
      return checkValue('nums', value);
    };

    $scope.isUniqueDocNumberPrefix = function (value) {
      return checkValue('nump', value);
    };

    $scope.isUniqueMedClass= function (value) {
      return checkValue('medclid', value);
    };
  }

  PersonMedicalCtrl.$inject = ['$scope', '$stateParams', 'PersonMedical'];

  angular.module('gva').controller('PersonMedicalCtrl', PersonMedicalCtrl);
}(angular));
