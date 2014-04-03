/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApplication', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/applications');
  }]);
}(angular));
