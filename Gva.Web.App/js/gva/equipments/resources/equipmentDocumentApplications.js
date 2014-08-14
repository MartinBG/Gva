/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentDocumentApplications', [
    '$resource',
    function ($resource) {
      return $resource('api/equipments/:id/equipmentDocumentApplications/:ind', {}, {
        newApplication: {
          method: 'GET',
          url: 'api/equipments/:id/equipmentDocumentApplications/new'
        }
      });
    }
  ]);
}(angular));
