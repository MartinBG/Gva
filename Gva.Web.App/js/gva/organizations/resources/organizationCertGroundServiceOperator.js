/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationCertGroundServiceOperators',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationCertGroundServiceOperators/:ind');
  }]);
}(angular));
