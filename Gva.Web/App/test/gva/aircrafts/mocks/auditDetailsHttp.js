/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', '/api/auditDetails?type',
          function ($params, auditPartRequirements, auditResults) {
            var auditDetails = [];
            if ($params.type === 'aircrafts') {
              _.each(auditPartRequirements, function (requirement) {
                auditDetails.push({
                  subject: requirement,
                  code: requirement.nomValueId,
                  auditResult: _.where(auditResults, { alias: 'Not executed' })[0]
                });
              });
            }
            return [200, auditDetails];
          });

  });
}(angular, _));
