/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Integration', ['$resource', function ($resource) {
    return $resource('api/integration/', {}, {
      getCaseApplications: {
        method: 'GET',
        url: 'api/integration/caseApplications',
        isArray: true
      },
      createLot: {
        method: 'POST',
        url: 'api/integration/createLot'
      },
      createApplication: {
        method: 'POST',
        url: 'api/integration/createApplication'
      },
      getEmptyIntegrationDocRelation: {
        method: 'GET',
        url: 'api/integration/emptyIntegrationDocRelation'
      }
    });
  }]);
}(angular));