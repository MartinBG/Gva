/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationDocumentApplication',
    ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/organizationDocumentApplications/:ind');
  }]);
}(angular));
