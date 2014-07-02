/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory(
      'EquipmentDocumentApplications',
      ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/equipmentDocumentApplications/:ind');
  }]);
}(angular));
