/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentOther', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id/equipmentDocumentOthers/:ind');
  }]);
}(angular));