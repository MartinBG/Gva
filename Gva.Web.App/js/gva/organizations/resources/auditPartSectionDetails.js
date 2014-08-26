/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AuditPartSectionDetails', ['$resource', function ($resource) {
    return $resource('api/audits/sectionDetails');
  }]);
}(angular));
