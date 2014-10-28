/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAwExaminers', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationAwExaminers/:ind', {}, {
      newAwExaminer: {
        method: 'GET',
        url: 'api/organizations/:id/organizationAwExaminers/new'
      }
    });
  }]);
}(angular));
