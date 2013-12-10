/*global angular*/
(function (angular) {
  'use strict';

  angular.module('users').factory('User', ['$resource', function ($resource) {
    return $resource('/api/users/:userId', {userId:'@userId'});
  }]);
}(angular));
