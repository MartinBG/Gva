/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonData', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personData', {id:'@id'});
  }]);
}(angular));