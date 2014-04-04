/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationInventory', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationInventory');
  }]);
}(angular));