/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirportOperators', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationCertAirportOperators/:ind', {}, {
      newCertAirportOperator: {
        method: 'GET',
        url: 'api/organizations/:id/organizationCertAirportOperators/new'
      }
    });
  }]);
}(angular));
