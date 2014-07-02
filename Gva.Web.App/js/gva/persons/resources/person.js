/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Persons', ['$resource', function ($resource) {
    return $resource('/api/persons/:id', {}, {
      'getNextLin': {
        method: 'GET',
        url: '/api/persons/nextLin'
      }
    });
  }]);
}(angular));
