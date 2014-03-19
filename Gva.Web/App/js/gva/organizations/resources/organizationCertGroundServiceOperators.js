/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationCertGroundServiceOperator',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/certGroundServiceOperators/:ind');
  }]);
}(angular));
