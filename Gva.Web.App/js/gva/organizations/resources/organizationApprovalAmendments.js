/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApprovalAmendments',
    ['$resource', function ($resource) {
      var routeString = 
        'api/organizations/:id/organizationApprovals/:ind/approvalAmendments/:index';
      return $resource(routeString, {}, {
        newApprovalAmendment: {
          method: 'GET',
          url: 'api/organizations/:id/approvalAmendments/new'
        }
      });
    }]);
}(angular));