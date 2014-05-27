/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Signal', ['$resource', function ($resource) {
    return $resource('/api/signals/:id');
  }]);
}(angular));
