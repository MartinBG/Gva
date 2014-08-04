/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicences', ['$resource', function ($resource) {
    return $resource('api/persons/:id/licences/:ind', {}, {
      'init': {
        method: 'GET',
        url: 'api/persons/:id/licences/init'
      }
    });
  }]);
}(angular));
