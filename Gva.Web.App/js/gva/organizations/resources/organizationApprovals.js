/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApprovals', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationApprovals/:ind');
  }]);
}(angular));
