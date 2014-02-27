/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Aircraft', ['$resource', function($resource) {
    return $resource('/api/aircrafts/:id');
  }]);
}(angular));
