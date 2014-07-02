/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    equipmentInspections) {

    $scope.equipmentInspections = equipmentInspections;

    $scope.newInspection = function () {
      return $state.go('root.equipments.view.inspections.new');
    };

    $scope.editInspection = function (inspection) {
      return $state.go('root.equipments.view.inspections.edit', {
        id: $stateParams.id,
        ind: inspection.partIndex
      });
    };

  }

  EquipmentsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'equipmentInspections'
  ];

  EquipmentsInspectionsSearchCtrl.$resolve = {
    equipmentInspections: [
      '$stateParams',
      'EquipmentInspections',
      function ($stateParams, EquipmentInspections) {
        return EquipmentInspections.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('EquipmentsInspectionsSearchCtrl', EquipmentsInspectionsSearchCtrl);
}(angular));
