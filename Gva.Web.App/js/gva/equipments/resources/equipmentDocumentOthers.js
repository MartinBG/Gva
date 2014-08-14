/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentOthers', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/equipmentDocumentOthers/:ind', {}, {
      newDocument: {
        method: 'GET',
        url: 'api/equipments/:id/equipmentDocumentOthers/new'
      }
    });
  }]);
}(angular));