/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentOwners', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/equipmentDocumentOwners/:ind');
  }]);
}(angular));