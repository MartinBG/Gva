/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirNavigationServiceDeliverers',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationCertAirNavigationServiceDeliverers/:ind');
  }]);
}(angular));
