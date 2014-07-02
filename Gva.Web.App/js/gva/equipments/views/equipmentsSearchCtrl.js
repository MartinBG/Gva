/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    equipments) {

    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.equipments = equipments;

    $scope.search = function () {
      $state.go('root.equipments.search', {
        name: $scope.filters.name
      });
    };

    $scope.newEquipment = function () {
      return $state.go('root.equipments.new');
    };

    $scope.viewEquipment = function (equipment) {
      return $state.go('root.equipments.view', { id: equipment.id });
    };
  }

  EquipmentsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'equipments'
  ];

  EquipmentsSearchCtrl.$resolve = {
    equipments: [
      '$stateParams',
      'Equipments',
      function ($stateParams, Equipments) {
        return Equipments.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsSearchCtrl', EquipmentsSearchCtrl);
}(angular, _));
