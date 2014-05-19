/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationApprovals',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationApprovals = _.map(organization.organizationApprovals,
            function (approval) {
              return {
                partIndex: approval.partIndex,
                approval: approval.part,
                amendment: approval.amendments[approval.amendments.length - 1],
                documentFirstDateIssue: approval.amendments[0].part.documentDateIssue
              };
            });

          return [200, organizationApprovals];
        })
      .when('POST', '/api/organizations/:id/organizationApprovals',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationApproval = {
            partIndex: organization.nextIndex++,
            part: $jsonData.approval.part,
            amendments: [{
              partIndex: organization.nextIndex++,
              part: $jsonData.amendment.part
            }]
          };

          organization.organizationApprovals.push(organizationApproval);

          return [200];
        });
  });
}(angular, _));