/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationStaffManagement', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/staffManagement/:ind');
  }]);
}(angular));
