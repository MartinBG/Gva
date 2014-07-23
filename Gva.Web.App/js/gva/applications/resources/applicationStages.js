/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AppStages', ['$resource', function ($resource) {
    return $resource('/api/apps/:id/stages/:ind');
  }]);
}(angular));
