/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationStaffExaminers',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.staffExaminers];
        })
      .when('GET', '/api/organizations/:id/organizationStaffExaminers/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffExaminer = _(organization.staffExaminers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (staffExaminer) {
            return [200, staffExaminer];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/organizationStaffExaminers',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffExaminer = $jsonData;

          staffExaminer.partIndex = organization.nextIndex++;

          organization.staffExaminers.push(staffExaminer);

          return [200];
        })
      .when('POST', '/api/organizations/:id/organizationStaffExaminers/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffExaminer = _(organization.staffExaminers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(staffExaminer, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/organizationStaffExaminers/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffExaminerInd = _(organization.staffExaminers)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.staffExaminers.splice(staffExaminerInd, 1);

          return [200];
        });
  });
}(angular, _));