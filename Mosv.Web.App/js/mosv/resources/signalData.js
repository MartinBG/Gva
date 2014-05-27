/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('SignalData', ['$resource', function ($resource) {
    return $resource('/api/signals/:id/signalData');
  }]);
}(angular));
