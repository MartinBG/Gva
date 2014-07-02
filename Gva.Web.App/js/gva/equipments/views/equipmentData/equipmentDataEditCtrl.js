/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentsData,
    equipmentData
  ) {
    var originalEquipmentData = _.cloneDeep(equipmentData);

    $scope.equipmentData = equipmentData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentData = _.cloneDeep(originalEquipmentData);
    };

    $scope.save = function () {
      return $scope.equipmentDataForm.$validate()
      .then(function () {
        if ($scope.equipmentDataForm.$valid) {
          return EquipmentsData
          .save({ id: $stateParams.id }, $scope.equipmentData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.equipments.view', $stateParams, { reload: true });
          });
        }
      });
    };
  }

  EquipmentDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentsData',
    'equipmentData'
  ];

  EquipmentDataEditCtrl.$resolve = {
    equipmentData: [
      '$stateParams',
      'EquipmentsData',
      function ($stateParams, EquipmentsData) {
        return EquipmentsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentDataEditCtrl', EquipmentDataEditCtrl);
}(angular, _));
