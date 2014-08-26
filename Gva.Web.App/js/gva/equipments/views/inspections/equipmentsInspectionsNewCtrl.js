/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentInspections,
    equipmentInspection
  ) {
    $scope.equipmentInspection = equipmentInspection;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return EquipmentInspections
            .save({ id: $stateParams.id }, $scope.equipmentInspection)
            .$promise
            .then(function (inspection) {
              return $state.go('root.equipments.view.inspections.edit', { 
                  id: $stateParams.id,
                  ind: inspection.partIndex
                });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.inspections.search');
    };
  }

  EquipmentsInspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentInspections',
    'equipmentInspection'
  ];

  EquipmentsInspectionsNewCtrl.$resolve = {
    equipmentInspection: [
      '$stateParams',
      'EquipmentInspections',
      function ($stateParams, EquipmentInspections) {
        return EquipmentInspections.newInspection({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsInspectionsNewCtrl', EquipmentsInspectionsNewCtrl);
}(angular));
