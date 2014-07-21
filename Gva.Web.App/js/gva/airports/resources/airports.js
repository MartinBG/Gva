/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Airports', ['$resource', function ($resource) {
    return $resource('api/airports/:id');
  }]);
}(angular));
