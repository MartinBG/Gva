/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Person', ['$resource', function($resource) {
    return $resource('/api/persons/:id', {id:'@id'});
  }]);
}(angular));