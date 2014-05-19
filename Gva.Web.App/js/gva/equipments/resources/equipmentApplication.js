/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentApplication', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/applications/:appId');
  }]);
}(angular));
