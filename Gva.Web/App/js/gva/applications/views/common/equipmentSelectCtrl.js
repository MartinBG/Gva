/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentSelectCtrl($scope, $state, $stateParams, Equipment, selectedEquipment) {
    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Equipment.query($stateParams).$promise.then(function (equipments) {
      $scope.equipments = equipments;
    });

    $scope.search = function () {
      $state.go($state.current, {
        name: $scope.filters.name
      }, { reload: true });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectEquipment = function (result) {
      selectedEquipment.push(result.id);
      return $state.go('^');
    };
  }

  EquipmentSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Equipment',
    'selectedEquipment'
  ];

  angular.module('gva').controller('EquipmentSelectCtrl', EquipmentSelectCtrl);
}(angular, _));
