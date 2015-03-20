/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common')

  .factory('ClassificationsResource', ['$resource', function ($resource) {
    return $resource('api/units/classifications');
  }]);

}(angular));
