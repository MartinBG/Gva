/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentInspection,
    equipmentInspection) {
    var originalDoc = _.cloneDeep(equipmentInspection);

    $scope.equipmentInspection = equipmentInspection;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return EquipmentInspection
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentInspection)
            .$promise
            .then(function () {
              return $state.go('root.equipments.view.inspections.search');
            });
        }
      });
    };


    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentInspection.part = _.cloneDeep(originalDoc.part);
    };
    
    $scope.deleteInspection = function () {
      return EquipmentInspection.remove({
        id: $stateParams.id,
        ind: equipmentInspection.partIndex
      }).$promise.then(function () {
        return $state.go('root.equipments.view.inspections.search');
      });
    };
  }

  EquipmentsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentInspection',
    'equipmentInspection'
  ];

  EquipmentsInspectionsEditCtrl.$resolve = {
    equipmentInspection: [
      '$stateParams',
      'EquipmentInspection',
      function ($stateParams, EquipmentInspection) {
        return EquipmentInspection.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsInspectionsEditCtrl', EquipmentsInspectionsEditCtrl);
}(angular, _));
