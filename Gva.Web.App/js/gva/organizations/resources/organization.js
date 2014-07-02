/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Organizations', ['$resource', function($resource) {
    return $resource('/api/organizations/:id');
  }]);
}(angular));
