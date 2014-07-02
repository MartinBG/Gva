/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApplications', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/applications/:appId');
  }]);
}(angular));
