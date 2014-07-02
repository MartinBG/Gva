/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentSelectCtrl($scope, $state, $stateParams, Equipments, selectedEquipment) {
    $scope.filters = {
      name: null
    };

    _.forOwn(_.pick($stateParams, ['name']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    Equipments.query($scope.filters).$promise.then(function (equipments) {
      $scope.equipments = equipments;
    });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectEquipment = function (result) {
      selectedEquipment.push(result.id);
      return $state.go('^');
    };

    $scope.viewEquipment = function (result) {
      return $state.go('root.equipments.view.edit', { id: result.id });
    };
  }

  EquipmentSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Equipments',
    'selectedEquipment'
  ];

  angular.module('gva').controller('EquipmentSelectCtrl', EquipmentSelectCtrl);
}(angular, _));
