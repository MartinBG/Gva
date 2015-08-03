/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicenceEditions', ['$resource', function ($resource) {
    return $resource('api/persons/:id/licences/:ind/licenceEditions/:index', {}, {
      newLicenceEdition: {
        method: 'GET',
        url: 'api/persons/:id/licenceEditions/new'
      }
    });
  }]);
}(angular));
