﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Organization', ['$resource', function($resource) {
    return $resource('/api/organizations/:id', {}, {
      'getCaseTypes': {
        method: 'GET',
        url: '/api/organizations/caseTypes'
      }
    });
  }]);
}(angular));