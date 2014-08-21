/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirCarriers', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationCertAirCarriers/:ind', {}, {
      newCertAirCarrier: {
        method: 'GET',
        url: 'api/organizations/:id/organizationCertAirCarriers/new'
      }
    });
  }]);
}(angular));
