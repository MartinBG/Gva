/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Equipment,
    equipment
  ) {
    $scope.equipment = equipment;

    $scope.edit = function () {
      return $state.go('root.equipments.view.edit');
    };
  }

  EquipmentsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Equipment',
    'equipment'
  ];

  EquipmentsViewCtrl.$resolve = {
    equipment: [
      '$stateParams',
      'Equipment',
      function ($stateParams, Equipment) {
        return Equipment.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsViewCtrl', EquipmentsViewCtrl);
}(angular));
