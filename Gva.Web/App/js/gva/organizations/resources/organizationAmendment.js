/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAmendment', ['$resource', function($resource) {
    return $resource('/api/organizations/:id/approvals/:ind/amendments/:childInd');
  }]);
}(angular));
