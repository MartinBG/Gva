﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationDocumentOthers', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationDocumentOthers/:ind', {}, {
      newDocument: {
        method: 'GET',
        url: 'api/organizations/:id/organizationDocumentOthers/new'
      }
    });
  }]);
}(angular));
