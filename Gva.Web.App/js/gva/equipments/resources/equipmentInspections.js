/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentInspections', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/inspections/:ind', {}, {
      newInspection: {
        method: 'GET',
        url: 'api/equipments/:id/inspections/new'
      }
    });
  }]);
}(angular));