﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationAuditplans', ['$resource', function ($resource) {
    return $resource('api/organizations/:id/organizationAuditplans/:ind');
  }]);
}(angular));
