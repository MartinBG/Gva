/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Signals', ['$resource', function ($resource) {
    return $resource('/api/signals/:id');
  }]);
}(angular));
