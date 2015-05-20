/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Aircrafts', ['$resource', function($resource) {
    return $resource('api/aircrafts/:id', {}, {
      'checkRegMark': {
        method: 'GET',
        url: 'api/aircrafts/checkRegMark'
      },
      'getNextActNumber': {
        method: 'GET',
        url: 'api/aircrafts/getNextActNumber'
      },
      'checkMSN': {
        method: 'GET',
        url: 'api/aircrafts/checkMSN'
      },
      newAircraft: {
        method: 'GET',
        url: 'api/aircrafts/new'
      },
      getRegistrations: {
        method: 'GET',
        url: 'api/aircrafts/registrations',
        isArray: true
      },
      getInvalidActNumbers: {
        method: 'GET',
        url: 'api/aircrafts/invalidActNumbers',
        isArray: true
      },
      devalidateActNumber: {
        method: 'POST',
        url: 'api/aircrafts/devalidateActNumber'
      },
      getNextFormNumber: {
        method: 'GET',
        url: 'api/aircrafts/nextFormNumber'
      },
      isUniqueFormNumber: {
        method: 'GET',
        url: 'api/aircrafts/uniqueFormNumber'
      }
    });
  }]);
}(angular));
