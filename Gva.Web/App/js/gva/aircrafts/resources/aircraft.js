/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Aircraft', ['$resource', function($resource) {
    return $resource('/api/aircrafts/:id', {}, {
      'checkRegMark': {
        method: 'POST',
        url: '/api/aircrafts/checkRegMark/:regMark',
        params: {
          regMark: '@regMark'
        }
      },
      'getNextCertNumber': {
        method: 'POST',
        url: '/api/aircrafts/getNextCertNumber'
      }
    });
  }]);
}(angular));
