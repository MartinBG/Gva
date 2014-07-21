/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('AdmissionsData', ['$resource', function ($resource) {
    return $resource('api/admissions/:id/admissionData');
  }]);
}(angular));
