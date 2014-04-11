/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    equipmentCertOper,
    availableDocuments,
    Nomenclature
  ) {
    $scope.availableDocuments = availableDocuments;

    if ($stateParams.documentTypes) {
      Nomenclature.query({alias: 'documentParts', set: 'equipment'})
      .$promise.then(function(documentTypes){
        $scope.documentParts = _.filter(documentTypes, function (type) {
          return _.contains($stateParams.documentTypes, type.alias);
        });
      });
    }

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }),
        function (document) {
          equipmentCertOper.part.includedDocuments.push({
            partIndex: document.partIndex,
            setPartAlias: document.setPartAlias
          });
        });
      return $state.go('^');
    };

    $scope.search = function () {
      return $state.go($state.current, {
        id: $stateParams.id,
        documentTypes: _.pluck($scope.documentParts, 'alias')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  EquipmentsChooseDocumentsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'equipmentCertOper',
    'availableDocuments',
    'Nomenclature'
  ];

  EquipmentsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'EquipmentInventory',
      'equipmentCertOper',
      function ($stateParams, EquipmentInventory, equipmentCertOper) {
        return EquipmentInventory
          .query({
            id: $stateParams.id,
            documentTypes: $stateParams.documentTypes? $stateParams.documentTypes.split(',') : null
          })
          .$promise
          .then(function (availableDocuments) {
            return _.reject(availableDocuments, function (availableDocument) {
              var count = _.where(equipmentCertOper.part.includedDocuments,
                { partIndex: availableDocument.partIndex }).length;
              return count > 0;
            });
          });
      }
    ]
  };

  angular.module('gva')
    .controller('EquipmentsChooseDocumentsCtrl', EquipmentsChooseDocumentsCtrl);
}(angular, _));
