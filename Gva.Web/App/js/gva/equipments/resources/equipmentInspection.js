/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentInspection', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/inspections/:ind');
  }]);
}(angular));