/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentInventory', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/inventory');
  }]);
}(angular));