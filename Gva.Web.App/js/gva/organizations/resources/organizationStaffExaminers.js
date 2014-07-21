/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationStaffExaminers', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationStaffExaminers/:ind');
  }]);
}(angular));
