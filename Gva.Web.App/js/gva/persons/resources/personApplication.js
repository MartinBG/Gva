/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonApplications', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/applications/:appId');
  }]);
}(angular));
