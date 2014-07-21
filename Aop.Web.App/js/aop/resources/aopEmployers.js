/*global angular*/
(function (angular) {
  'use strict';

  angular.module('aop')
    .factory('AopEmployers', ['$resource',
      function ($resource) {
        return $resource('api/aop/emps/:id', { id: '@aopEmployerId' });
      }]);
}(angular));
