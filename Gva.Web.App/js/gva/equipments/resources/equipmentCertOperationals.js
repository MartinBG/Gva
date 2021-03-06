﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('EquipmentCertOperationals', ['$resource', function ($resource) {
    return $resource('api/equipments/:id/equipmentCertOperationals/:ind', {}, {
      newCertOperational: {
        method: 'GET',
        url: 'api/equipments/:id/equipmentCertOperationals/new'
      }
    });
  }]);
}(angular));