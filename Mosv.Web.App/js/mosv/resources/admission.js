/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Admissions', ['$resource', function($resource) {
    return $resource('/api/admissions/:id');
  }]);
}(angular));
