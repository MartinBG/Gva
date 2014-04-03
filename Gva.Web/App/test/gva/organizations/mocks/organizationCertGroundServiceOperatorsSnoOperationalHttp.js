/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.certGroundServiceOperatorsSnoOperational];
        })
      .when('GET', '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var cert = _(organization.certGroundServiceOperatorsSnoOperational)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (cert) {
            return [200, cert];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var cert = $jsonData;

          cert.partIndex = organization.nextIndex++;

          organization.certGroundServiceOperatorsSnoOperational.push(cert);

          return [200];
        })
      .when('POST', '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var cert = _(organization.certGroundServiceOperatorsSnoOperational)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(cert, $jsonData);

          return [200];
        })
      .when('DELETE',
      '/api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certInd = _(organization.certGroundServiceOperatorsSnoOperational)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.certGroundServiceOperatorsSnoOperational.splice(certInd, 1);

          return [200];
        });
  });
}(angular, _));