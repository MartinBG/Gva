/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentsData', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/equipmentData');
  }]);
}(angular));
