/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems').factory('Units', ['$resource', function ($resource) {
    return $resource('api/noms/units/:id', {}, {
      getNew: {
        method: 'GET',
        url: 'api/noms/units/new'
      }
    });
  }]);
}(angular));
