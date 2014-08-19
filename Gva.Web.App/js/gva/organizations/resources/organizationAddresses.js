/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAddresses', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationAddresses/:ind', {}, {
      newAddress: {
        method: 'GET',
        url: 'api/organizations/:id/organizationAddresses/new'
      }
    });
  }]);
}(angular));
