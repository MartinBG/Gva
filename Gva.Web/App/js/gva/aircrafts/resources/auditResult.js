/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AuditResult', ['$resource', function ($resource) {
    return $resource('/api/auditResults');
  }]);
}(angular));
