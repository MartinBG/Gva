/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationApprovals', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationApprovals/:ind', {}, {
      newApproval: {
        method: 'GET',
        url: 'api/organizations/:id/organizationApprovals/new'
      },
      newApprovalAmendment: {
        method: 'GET',
        url: 'api/organizations/:id/organizationApprovals/:ind/newAmendment'
      }
    });
  }]);
}(angular));
