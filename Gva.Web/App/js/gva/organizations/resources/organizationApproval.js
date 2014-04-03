/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApproval', ['$resource', function($resource) {
    return $resource('/api/organizations/:id/organizationApprovals');
  }]);
}(angular));
