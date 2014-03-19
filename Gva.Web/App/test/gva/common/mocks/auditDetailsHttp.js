/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', '/api/auditDetails?type',
          function ($params, auditPartRequirements, auditResults) {
            var auditDetails = [],
              defaultAuditResult = _.where(auditResults, { alias: 'Not executed' })[0],
              requirements = _.where(auditPartRequirements, { type: $params.type });

            _.each(requirements, function (requirement) {
              auditDetails.push({
                subject: requirement,
                code: requirement.nomValueId,
                auditResult: defaultAuditResult
              });
            });
            return [200, auditDetails];
          });

  });
}(angular, _));
