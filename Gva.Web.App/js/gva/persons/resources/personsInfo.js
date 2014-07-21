/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsInfo', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personInfo');
  }]);
}(angular));
