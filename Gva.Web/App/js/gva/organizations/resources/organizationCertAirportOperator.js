/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirportOperator', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationCertAirportOperators/:ind');
  }]);
}(angular));
