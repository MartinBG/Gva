/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AuditPartRequirement', ['$resource', function ($resource) {
    return $resource('/api/auditPartRequirements');
  }]);
}(angular));
