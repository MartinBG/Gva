/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    equipmentInspections) {
    $scope.equipmentInspections = equipmentInspections;
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
