/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationDocumentOther', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/documentOthers/:ind');
  }]);
}(angular));
