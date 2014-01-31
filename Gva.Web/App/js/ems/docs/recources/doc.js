﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Doc', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:docId', { docId: '@docId' },
          {
            'getNew': {
              method: 'GET',
              url: '/api/docs/new'
            }
          });
      }]);
}(angular));
