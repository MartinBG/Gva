/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectEquipmentCtrl($scope, scModal) {
    $scope.chooseEquipment = function () {
      var modalInstance = scModal.open('chooseEquipment');

      modalInstance.result.then(function (equipmentId) {
        $scope.model.lot.id = equipmentId;
      });

      return modalInstance.opened;
    };

      $scope.newEquipment = function () {
        var modalInstance = scModal.open('newEquipment');

        modalInstance.result.then(function (equipmentId) {
          $scope.model.lot.id = equipmentId;
        });

        return modalInstance.opened;
      };
  }

  AppSelectEquipmentCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppSelectEquipmentCtrl', AppSelectEquipmentCtrl);
}(angular));
