/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationData', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationData');
  }]);
}(angular));
