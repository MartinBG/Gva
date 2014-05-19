/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAuditplan', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationAuditplans/:ind');
  }]);
}(angular));
