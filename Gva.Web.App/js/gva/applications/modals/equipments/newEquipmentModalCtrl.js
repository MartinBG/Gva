/*global angular*/
(function (angular) {
  'use strict';

  function NewEquipmentModalCtrl(
    $scope,
    $modalInstance,
    Equipments,
    equipment
  ) {
    $scope.form = {};
    $scope.equipment = equipment;

    $scope.save = function () {
      return $scope.form.newEquipmentForm.$validate().then(function () {
        if ($scope.form.newEquipmentForm.$valid) {
          return Equipments.save($scope.equipment).$promise.then(function (savedEquipment) {
            return $modalInstance.close(savedEquipment.id);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewEquipmentModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Equipments',
    'equipment'
  ];

  NewEquipmentModalCtrl.$resolve = {
    equipment: [
      'Equipments',
      function (Equipments) {
        return Equipments.newEquipment().$promise;
      }
    ]
  };

  angular.module('gva').controller('NewEquipmentModalCtrl', NewEquipmentModalCtrl);
}(angular));
