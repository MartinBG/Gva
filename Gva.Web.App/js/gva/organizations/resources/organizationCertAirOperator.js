/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirOperator', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationCertAirOperators/:ind');
  }]);
}(angular));
