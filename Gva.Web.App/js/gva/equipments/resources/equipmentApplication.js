/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentApplications', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/applications/:appId');
  }]);
}(angular));
