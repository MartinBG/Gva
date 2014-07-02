/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('SignalsData', ['$resource', function ($resource) {
    return $resource('/api/signals/:id/signalData');
  }]);
}(angular));
