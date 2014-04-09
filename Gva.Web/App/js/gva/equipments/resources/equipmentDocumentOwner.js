/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentOwner', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/equipmentDocumentOwners/:ind');
  }]);
}(angular));