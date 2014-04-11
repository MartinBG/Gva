/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicence', ['$resource', function($resource) {
    return $resource('/api/persons/:id/licences/:ind');
  }]);
}(angular));
