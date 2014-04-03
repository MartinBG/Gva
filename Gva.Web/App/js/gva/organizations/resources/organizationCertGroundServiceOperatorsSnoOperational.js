/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationCertGroundServiceOperatorsSnoOperational',
    ['$resource', function ($resource) {
      var path = '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/:ind';
      return $resource(path);
    }]);
}(angular));
