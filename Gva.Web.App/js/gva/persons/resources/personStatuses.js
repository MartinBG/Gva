﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('PersonStatuses', ['$resource', function ($resource) {
      return $resource('api/persons/:id/personStatuses/:ind', {}, {
        newStatus: {
          method: 'GET',
          url: 'api/persons/:id/personStatuses/new'
        }
      });
    }]);
}(angular));
