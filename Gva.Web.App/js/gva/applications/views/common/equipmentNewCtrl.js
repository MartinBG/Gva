/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentNewCtrl($scope, $state, Equipment, equipment, selectedEquipment) {
    $scope.equipment = equipment;

    $scope.save = function () {
      return $scope.newEquipmentForm.$validate()
      .then(function () {
        if ($scope.newEquipmentForm.$valid) {
          return Equipment.save($scope.equipment).$promise
            .then(function (equipment) {
              selectedEquipment.push(equipment.id);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  EquipmentNewCtrl.$inject = ['$scope', '$state', 'Equipment', 'equipment', 'selectedEquipment'];

  EquipmentNewCtrl.$resolve = {
    equipment: function () {
      return {
        equipmentData: {
          caseTypes: [
            {
              nomValueId: 3
            }
          ]
        }
      };
    }
  };

  angular.module('gva').controller('EquipmentNewCtrl', EquipmentNewCtrl);
}(angular));
