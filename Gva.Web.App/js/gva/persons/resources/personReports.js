/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonReports', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personReports/:ind', {}, {
      newReport: {
        method: 'GET',
        url: 'api/persons/:id/personReports/new'
      }
    });
  }]);
}(angular));
