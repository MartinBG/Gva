/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationInspections', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationInspections/:ind', {}, {
      getRecommendations: {
        method: 'GET',
        url: 'api/organizations/:id/organizationInspections/:ind/recommendations',
        isArray: true
      },
      newInspection: {
        method: 'GET',
        url: 'api/organizations/:id/organizationInspections/new'
      },
      getInspectionsData: {
        method: 'GET',
        url: 'api/organizations/:id/organizationInspections/data',
        isArray: true
      }
    });
  }]);
}(angular));