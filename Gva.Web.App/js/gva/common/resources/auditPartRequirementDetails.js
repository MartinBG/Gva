/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AuditPartRequirementDetails', ['$resource', function ($resource) {
    return $resource('api/audits/requirementDetails');
  }]);
}(angular));
