/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('ApplicationNoms', ['$resource', function ($resource) {
    return $resource('api/nomenclatures/:lotId/applications/:appId');
  }]);
}(angular));
