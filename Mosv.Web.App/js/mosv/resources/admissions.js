﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Admissions', ['$resource', function ($resource) {
    return $resource('api/admissions/:id', {}, {
      'fastSave': {
        method: 'POST',
        url: 'api/admissions/:id/fastSave'
      },
      'getDocs': {
        method: 'GET',
        url: 'api/docs/forSelect'
      },
      'loadData': {
        method: 'POST',
        url: 'api/admissions/:id/loadData'
      },
      'findDocLotLink': {
        method: 'GET',
        url: 'api/admissions/:id/getDoc'
      },
      'createDocLotLink': {
        method: 'POST',
        url: 'api/admissions/:id/createLink',
        params: {
          lotType: ''
        }
      }
    });
  }]);
}(angular));
