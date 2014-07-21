/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationStaffManagements', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationStaffManagement/:ind');
  }]);
}(angular));
