/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('User', ['$resource', function ($resource) {
    return $resource('/api/users/:userId', {userId:'@userId'});
  }]);
}(angular));
