/*global angular, require*/
(function (angular) {
  'use strict';

  function PersonDocumentMedicalCtrl($scope) {
    var nomenclatures = require('./nomenclatures.sample');
    $scope.limitationsTypes = nomenclatures.medicalLimitationTypes.map(function (limitation) {
      limitation.text = limitation.name;
      limitation.id = limitation.nomTypeValueId;
      return limitation;
    });
  }

  PersonDocumentMedicalCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonDocumentMedicalCtrl', PersonDocumentMedicalCtrl);
}(angular));
