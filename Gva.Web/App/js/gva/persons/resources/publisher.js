/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Publisher', ['$resource', function($resource) {
    return $resource('/api/publishers');
  }]);
}(angular));
