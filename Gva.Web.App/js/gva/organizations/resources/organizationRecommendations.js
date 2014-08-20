/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationRecommendations', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationRecommendations/:ind', {}, {
      newRecommendation: {
        method: 'GET',
        url: 'api/organizations/:id/organizationRecommendations/new'
      }
    });
  }]);
}(angular));