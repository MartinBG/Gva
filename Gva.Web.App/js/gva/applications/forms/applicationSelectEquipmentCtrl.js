/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectEquipmentCtrl($scope, namedModal) {
    $scope.chooseEquipment = function () {
      var modalInstance = namedModal.open('chooseEquipment');

      modalInstance.result.then(function (equipmentId) {
        $scope.model.lot.id = equipmentId;
      });

      return modalInstance.opened;
    };

      $scope.newEquipment = function () {
        var modalInstance = namedModal.open('newEquipment');

        modalInstance.result.then(function (equipmentId) {
          $scope.model.lot.id = equipmentId;
        });

        return modalInstance.opened;
      };
  }

  AppSelectEquipmentCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('AppSelectEquipmentCtrl', AppSelectEquipmentCtrl);
}(angular));
