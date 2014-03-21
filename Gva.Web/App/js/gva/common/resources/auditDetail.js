/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AuditDetails', ['$resource', function ($resource) {
    return $resource('/api/auditDetails');
  }]);
}(angular));
