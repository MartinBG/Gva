/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonInfo', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personInfo');
  }]);
}(angular));
