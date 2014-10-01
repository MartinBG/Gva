/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicences', ['$resource', function ($resource) {
    return $resource('api/persons/:id/licences/:ind', {}, {
      newLicence: {
        method: 'GET',
        url: 'api/persons/:id/licences/new'
      },
      lastLicenceNumber: {
        method: 'GET',
        url: 'api/persons/:id/licences/lastLicenceNumber'
      },
      newLicenceStatus: {
        method: 'GET',
        url: 'api/persons/:id/licences/newStatus'
      },
      lastEditionIndex: {
        method: 'GET',
        url: 'api/persons/:id/licences/:ind/lastEditionIndex'
      }
    });
  }]);
}(angular));
