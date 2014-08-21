/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicences', ['$resource', function ($resource) {
    return $resource('api/persons/:id/licences/:ind', {}, {
      newLicence: {
        method: 'GET',
        url: 'api/persons/:id/licences/new'
      },
      newLicenceEdition: {
        method: 'GET',
        url: 'api/persons/:id/licences/:ind/newEdition'
      },
      lastLicenceNumber: {
        method: 'GET',
        url: 'api/persons/:id/licences/lastLicenceNumber'
      },      
      newLicenceStatus: {
        method: 'GET',
        url: 'api/persons/:id/licences/newStatus'
      }
    });
  }]);
}(angular));
