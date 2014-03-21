/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationStaffExaminer', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/staffExaminers/:ind');
  }]);
}(angular));
