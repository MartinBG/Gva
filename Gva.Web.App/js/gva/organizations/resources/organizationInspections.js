/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationInspections', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationInspections/:ind');
  }]);
}(angular));