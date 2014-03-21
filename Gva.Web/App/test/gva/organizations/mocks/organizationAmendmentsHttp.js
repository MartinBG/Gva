/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/approvals/:ind/amendments',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
           .filter({ lotId: parseInt($params.id, 10) }).first();

          var approval = _(organization.organizationApprovals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var amendments = _.map(approval.amendments, function (amendment) {
            return {
              partIndex: amendment.partIndex,
              approval: approval.part,
              amendment: amendment
            };
          });

          return [200, amendments];
        })
      .when('GET', '/api/organizations/:id/approvals/:ind/amendments/:childInd',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
           .filter({ lotId: parseInt($params.id, 10) }).first();

          var approval = _(organization.organizationApprovals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var amendment = _(approval.amendments)
            .filter({ partIndex: parseInt($params.childInd, 10) }).first();

          return [200, {
            partIndex: amendment.partIndex,
            approval: approval,
            amendment: amendment
          }];
        })
      .when('POST', '/api/organizations/:id/approvals/:ind/amendments',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var approval = _(organization.organizationApprovals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var organizationAmendment = {
            partIndex: organization.nextIndex++,
            part: $jsonData.amendment
          };

          approval.amendments.push(organizationAmendment);

          return [200];
        })
      .when('POST', '/api/organizations/:id/approvals/:ind/amendments/:childInd',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var approval = _(organization.organizationApprovals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var amendment = _(approval.amendments)
            .filter({ partIndex: parseInt($params.childInd, 10) }).first();

          _.assign(approval.part, $jsonData.approval.part);
          _.assign(amendment.part, $jsonData.amendment.part);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/approvals/:ind/amendments/:childInd',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var approval = _(organization.organizationApprovals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var amendmentChildInd = _(approval.amendments)
            .findIndex({ partIndex: parseInt($params.childInd, 10) });

          approval.amendments.splice(amendmentChildInd, 1);

          if (approval.amendments === []) {
            var approvalInd = _(approval.organizationApprovals)
           .findIndex({ partIndex: parseInt($params.ind, 10) });

            organization.organizationApprovals.splice(approvalInd, 1);
          }
          return [200];
        });
  });
}(angular, _));