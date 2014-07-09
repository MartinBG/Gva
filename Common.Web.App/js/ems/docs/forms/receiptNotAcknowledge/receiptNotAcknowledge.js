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

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise
        .then(function (result) {
          var nomId = null;

          if (!!$scope.model.jObject
            .ElectronicServiceProvider.ElectronicServiceProviderType) {
            nomId = _(result).filter({
              code: $scope.model.jObject
                .ElectronicServiceProvider.ElectronicServiceProviderType
            }).first().nomValueId;
          }

          $scope.serviceProvider = {
            obj: {},
            id: nomId
          };

          $scope.discrepancies = [];

          _($scope.model.jObject.Discrepancies.DiscrepancyCollection).forEach(function (item) {
            $scope.discrepancies.push({
              text: item
            });
          });

          $scope.isLoaded = true;
        });
      });

    $scope.serviceProviderChange = function () {
      $scope.model.jObject.ElectronicServiceProvider.ElectronicServiceProviderType =
        $scope.serviceProvider.obj.code;
      $scope.model.jObject.ElectronicServiceProvider.EntityBasicData.Identifier =
        $scope.serviceProvider.obj.bulstat;
      $scope.model.jObject.ElectronicServiceProvider.EntityBasicData.Name =
        $scope.serviceProvider.obj.name;
    };

    $scope.addDiscrepancy = function () {
      $scope.discrepancies.push({
        text: ''
      });
      $scope.model.jObject.Discrepancies.DiscrepancyCollection.push('');
    };

    $scope.removeDiscrepancy = function (index) {
      $scope.discrepancies.splice(index, 1); 
      $scope.model.jObject.Discrepancies.DiscrepancyCollection.splice(index, 1);
    };

    $scope.editDiscrepancy = function (index) {
      $scope.model.jObject.Discrepancies.DiscrepancyCollection[index] =
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
