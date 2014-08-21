/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Organizations', ['$resource', function($resource) {
    return $resource('api/organizations/:id', {}, {
      newOrganization: {
        method: 'GET',
        url: 'api/organizations/new'
      }
    });
  }]);
}(angular));
