/*global angular*/
(function (angular) {
  'use strict';

  function ChooseEquipmentModalCtrl(
    $scope,
    $modalInstance,
    Equipments,
    equipments
  ) {
    $scope.equipments = equipments;

    $scope.filters = {
      name: null
    };

    $scope.search = function () {
      return Equipments.query($scope.filters).$promise.then(function (equipments) {
        $scope.equipments = equipments;
      });
    };

    $scope.selectEquipment = function (equipment) {
      return $modalInstance.close(equipment.id);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseEquipmentModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Equipments',
    'equipments'
  ];

  angular.module('gva').controller('ChooseEquipmentModalCtrl', ChooseEquipmentModalCtrl);
}(angular));
