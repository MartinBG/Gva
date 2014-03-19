/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationInspection', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/inspections/:ind');
  }]);
}(angular));