/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationCertGroundServiceOperatorsSnoOperational',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/certGroundServiceOperatorsSnoOperational/:ind');
  }]);
}(angular));
