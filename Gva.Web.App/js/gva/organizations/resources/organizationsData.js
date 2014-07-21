/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationsData', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationData');
  }]);
}(angular));
