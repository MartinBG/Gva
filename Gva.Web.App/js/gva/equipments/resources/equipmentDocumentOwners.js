/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentOwners', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/equipmentDocumentOwners/:ind', {}, {
      newOwner: {
        method: 'GET',
        url: 'api/equipments/:id/equipmentDocumentOwners/new'
      }
    });
  }]);
}(angular));