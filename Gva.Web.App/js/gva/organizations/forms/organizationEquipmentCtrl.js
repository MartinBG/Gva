/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationEquipmentCtrl($scope) {

    $scope.deleteEquipment = function removeDocument(equipment) {
      var index = $scope.model.ssno.indexOf(equipment);
      $scope.model.ssno.splice(index, 1);
    };

    $scope.addEquipment = function () {
      $scope.model.ssno.push({});
    };

  }

  angular.module('gva')
    .controller('OrganizationEquipmentCtrl', OrganizationEquipmentCtrl);
}(angular));