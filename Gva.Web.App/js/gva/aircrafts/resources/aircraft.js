/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Aircraft', ['$resource', function($resource) {
    return $resource('/api/aircrafts/:id', {}, {
      'checkRegMark': {
        method: 'GET',
        url: '/api/aircrafts/checkRegMark'
      },
      'getNextActNumber': {
        method: 'GET',
        url: '/api/aircrafts/getNextActNumber'
      },
      'checkMSN': {
        method: 'GET',
        url: '/api/aircrafts/checkMSN'
      }
    });
  }]);
}(angular));
