/*global angular*/
(function (angular) {
  'use strict';

  angular.module('persons').factory('persons.Person', ['$resource', function($resource) {
    return $resource('/api/persons/:id', {id:'@id'});
  }]);
}(angular));