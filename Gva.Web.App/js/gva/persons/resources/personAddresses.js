/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonAddresses', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personAddresses/:ind', {}, {
      newAddress: {
        method: 'GET',
        url: 'api/persons/:id/personAddresses/new'
      }
    });
  }]);
}(angular));
