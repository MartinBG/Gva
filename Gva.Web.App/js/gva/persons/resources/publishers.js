/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Publishers', ['$resource', function($resource) {
    return $resource('api/publishers');
  }]);
}(angular));
