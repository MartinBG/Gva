/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('CertAirNavigationServiceDeliverer',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationCertAirNavigationServiceDeliverers/:ind');
  }]);
}(angular));
