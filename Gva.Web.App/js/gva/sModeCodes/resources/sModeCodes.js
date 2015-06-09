/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('SModeCodes', ['$resource', function ($resource) {
    return $resource('api/sModeCodes/:id', {}, {
      newSModeCode: {
        method: 'GET',
        url: 'api/sModeCodes/new'
      },
      getNextCode: {
        method: 'GET',
        url: 'api/sModeCodes/nextCode'
      },
      getSModeCodesPerAircraft: {
        method: 'GET',
        url: 'api/sModeCodes/perAircraft',
        isArray: true
      }
    });
  }]);
}(angular));
