/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Persons', ['$resource', function ($resource) {
    return $resource('api/persons/:id', {}, {
      getNextLin: {
        method: 'GET',
        url: 'api/persons/nextLin'
      },
      isUniqueUin: {
        method: 'GET',
        url: 'api/persons/isUniqueUin'
      },
      newPerson: {
        method: 'GET',
        url: 'api/persons/new'
      }
    });
  }]);
}(angular));
