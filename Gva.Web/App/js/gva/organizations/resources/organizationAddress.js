/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAddress', ['$resource', function($resource) {
    return $resource('/api/organizations/:id/organizationAddresses/:ind');
  }]);
}(angular));
