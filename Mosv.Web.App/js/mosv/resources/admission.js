/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Admission', ['$resource', function($resource) {
    return $resource('/api/admissions/:id');
  }]);
}(angular));
