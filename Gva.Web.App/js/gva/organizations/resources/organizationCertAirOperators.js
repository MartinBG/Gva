/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirOperators', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationCertAirOperators/:ind', {}, {
      newCertAirOperator: {
        method: 'GET',
        url: 'api/organizations/:id/organizationCertAirOperators/new'
      }
    });
  }]);
}(angular));
