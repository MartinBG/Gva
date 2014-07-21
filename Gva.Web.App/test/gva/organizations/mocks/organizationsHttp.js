/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function organizationMapper(organization) {
      return {
        id: organization.lotId,
        name: organization.organizationData.part.name,
        CAO: organization.organizationData.part.CAO,
        uin: organization.organizationData.part.uin,
        organizationType: organization.organizationData.part.organizationType.name,
        dateCAOValidTo: organization.organizationData.part.dateCAOValidTo,
        dateValidTo: organization.organizationData.part.dateValidTo,
        valid: organization.organizationData.part.valid.name
      };
    }

    $httpBackendConfiguratorProvider
      .when('GET', 'api/organizations?code',
        function ($params, $filter, organizationLots) {
          var organization = _(organizationLots)
           .map(organizationMapper)
           .filter(function (p) {
              var isMatch = true;

              _.forOwn($params, function (value, param) {
                if (!value) {
                  return;
                }
                isMatch = isMatch && p[param] && p[param].toString()
                  .indexOf($params[param]) >= 0;

                //short circuit forOwn if not a match
                return isMatch;
              });

              return isMatch;
            })
           .value();

          return [200, organization];
        })
      .when('GET', 'api/organizations/:id',
        function ($params, $filter, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).map(organizationMapper).first();

          if (organization) {
            return [200, organization];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations',
        function ($params, $jsonData, organizationLots) {
          var nextLotId = Math.max(_(organizationLots).pluck('lotId').max().value() + 1, 1);

          var newOrganization = {
            lotId: nextLotId,
            nextIndex: 4,
            organizationData: {
              partIndex: 1,
              part: $jsonData.organizationData
            }
          };

          organizationLots.push(newOrganization);

          return [200, newOrganization];
        });

  });
}(angular, _));