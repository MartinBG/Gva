/*global angular, require*/
(function (angular) {
  'use strict';

  function PersonDocumentMedicalCtrl($scope) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true : false);
    };
    var nomenclatures = require('./nomenclatures.sample');
    $scope.limitationsTypes = nomenclatures.medicalLimitationTypes.map(function (limitation) {
      limitation.text = limitation.name;
      limitation.id = limitation.nomTypeValueId;
      return limitation;
    });

  }

  PersonDocumentMedicalCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentMedical'];

  angular.module('gva').controller('PersonDocumentMedicalCtrl', PersonDocumentMedicalCtrl);
}(angular));
