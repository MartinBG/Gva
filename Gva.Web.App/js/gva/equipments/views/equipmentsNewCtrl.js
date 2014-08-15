/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsNewCtrl($scope, $state, Equipments, equipment) {
    $scope.equipment = equipment;

    $scope.save = function () {
      return $scope.newEquipmentForm.$validate()
      .then(function () {
        if ($scope.newEquipmentForm.$valid) {
          return Equipments.save($scope.equipment).$promise
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

  EquipmentsNewCtrl.$inject = ['$scope', '$state', 'Equipments', 'equipment'];

  EquipmentsNewCtrl.$resolve = {
    equipment: [
      'Equipments',
      function (Equipments) {
        return Equipments.newEquipment().$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsNewCtrl', EquipmentsNewCtrl);
}(angular));
