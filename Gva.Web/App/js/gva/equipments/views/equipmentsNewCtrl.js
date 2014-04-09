/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsNewCtrl($scope, $state, Equipment, equipment) {
    $scope.equipment = equipment;

    $scope.save = function () {
      return $scope.newEquipmentForm.$validate()
      .then(function () {
        if ($scope.newEquipmentForm.$valid) {
          return Equipment.save($scope.equipment).$promise
            .then(function () {
              return $state.go('root.equipments.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.search');
    };
  }

  EquipmentsNewCtrl.$inject = ['$scope', '$state', 'Equipment', 'equipment'];

  EquipmentsNewCtrl.$resolve = {
    equipment: function () {
      return {
        equipmentData: {
          caseTypes: [
            {
              nomValueId: 4
            }
          ],
          frequencies: [],
          radioNavigationAids: []
        }
      };
    }
  };

  angular.module('gva').controller('EquipmentsNewCtrl', EquipmentsNewCtrl);
}(angular));
