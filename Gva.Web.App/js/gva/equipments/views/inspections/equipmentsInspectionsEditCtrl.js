/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentInspections,
    equipmentInspection,
    scMessage
  ) {
    var originalInspection = _.cloneDeep(equipmentInspection);
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.equipmentInspection = equipmentInspection;
    $scope.lotId = $stateParams.lotId;

    $scope.editMode = null;
    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentInspection = _.cloneDeep(originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return EquipmentInspections
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentInspection)
            .$promise
            .then(function () {
              return $state.go('root.equipments.view.inspections.search');
            });
        }
      });
    };

    $scope.deleteInspection = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return EquipmentInspections.remove({
            id: $stateParams.id,
            ind: $stateParams.ind
          }).$promise.then(function () {
            return $state.go('root.equipments.view.inspections.search');
          });
        }
      });
    };
  }

  EquipmentsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentInspections',
    'equipmentInspection',
    'scMessage'
  ];

  EquipmentsInspectionsEditCtrl.$resolve = {
    equipmentInspection: [
      '$stateParams',
      'EquipmentInspections',
      function ($stateParams, EquipmentInspections) {
        return EquipmentInspections.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsInspectionsEditCtrl', EquipmentsInspectionsEditCtrl);
}(angular, _));
