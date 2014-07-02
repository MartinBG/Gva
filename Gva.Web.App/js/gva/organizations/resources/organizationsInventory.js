/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationsInventory', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationInventory');
  }]);
}(angular));