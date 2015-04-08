/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common')

  .factory('ClassificationPermissionsResource', ['$resource', function ($resource) {
    return $resource('api/units/classificationPermissions');    
  }]);

}(angular));
