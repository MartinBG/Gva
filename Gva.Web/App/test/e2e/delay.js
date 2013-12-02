/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').factory('$delay', ['$timeout', function ($timeout) {
    return function (delay, value) {
      return $timeout(function() {
        return value;
      }, delay);
    };
  }]);
}(angular));
