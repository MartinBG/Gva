/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ReceiptNotAcknowledgeCtrl(
    $scope,
    Docs,
    Nomenclatures
  ) {

    Docs.getRioEditableFile({
      id: $scope.model.docId
    }).$promise.then(function (result) {
      $scope.model.jObject = result.content;

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise.then(function (result) {
        var nomId = null;

        if ($scope.model.jObject.electronicServiceProvider.electronicServiceProviderType) {
          nomId = _(result).filter({
            code: $scope.model.jObject.electronicServiceProvider.electronicServiceProviderType
          }).first().nomValueId;
        }

        $scope.serviceProvider = {
          obj: {},
          id: nomId
        };

        $scope.discrepancies = [];

        _($scope.model.jObject.discrepancies.discrepancyCollection).forEach(function (item) {
          $scope.discrepancies.push({
            text: item
          });
        });

        $scope.isLoaded = true;
      });
    });

    $scope.serviceProviderChange = function () {
      $scope.model.jObject.electronicServiceProvider.electronicServiceProviderType =
        $scope.serviceProvider.obj.code;
      $scope.model.jObject.electronicServiceProvider.entityBasicData.identifier =
        $scope.serviceProvider.obj.bulstat;
      $scope.model.jObject.electronicServiceProvider.entityBasicData.name =
        $scope.serviceProvider.obj.name;
    };

    $scope.addDiscrepancy = function () {
      $scope.discrepancies.push({
        text: ''
      });
      $scope.model.jObject.discrepancies.discrepancyCollection.push('');
    };

    $scope.removeDiscrepancy = function (index) {
      $scope.discrepancies.splice(index, 1); 
      $scope.model.jObject.discrepancies.discrepancyCollection.splice(index, 1);
    };

    $scope.editDiscrepancy = function (index) {
      $scope.model.jObject.discrepancies.discrepancyCollection[index] =
        $scope.discrepancies[index].text;
    };
  }

  ReceiptNotAcknowledgeCtrl.$inject = [
    '$scope',
    'Docs',
    'Nomenclatures'
  ];

  angular.module('ems').controller('ReceiptNotAcknowledgeCtrl', ReceiptNotAcknowledgeCtrl);
}(angular, _));
