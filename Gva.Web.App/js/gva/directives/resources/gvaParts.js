/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('GvaParts', ['$resource', function ($resource) {
    return $resource('/api/gvaParts/:lotId/:partPath');
  }]);
}(angular));