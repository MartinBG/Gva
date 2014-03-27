/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationRegGroundServiceOperator',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationRegGroundServiceOperators/:ind');
  }]);
}(angular));
