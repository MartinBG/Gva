/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentCertOperational', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/equipmentCertOperationals/:ind');
  }]);
}(angular));