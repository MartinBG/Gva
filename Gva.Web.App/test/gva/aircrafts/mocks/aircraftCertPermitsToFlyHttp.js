/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function sortByDate(a, b) {
      if (a.part.validToDate > b.part.validToDate) {
        return -1;
      } else if (a.part.validToDate < b.part.validToDate) {
        return 1;
      } else {
        return 0;
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/aircraftCertPermitsToFly',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertPermitsToFly =
            aircraft.aircraftCertPermitsToFly.sort(sortByDate);
          return [200, aircraft.aircraftCertPermitsToFly];
        })
      .when('GET', 'api/aircrafts/:id/aircraftCertPermitsToFly/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var permits = aircraft.aircraftCertPermitsToFly.sort(sortByDate),
            permit;
          if ($params.ind && $params.ind !== 'current') {
            permit = _(permits)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            permit = permits.length > 0 ? permits[0] : null;
          }

          if (permit || $params.ind === 'current') {
            return [200, permit];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertPermitsToFly',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var permit = $jsonData;

          permit.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertPermitsToFly.push(permit);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertPermitsToFly/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var permit = _(aircraft.aircraftCertPermitsToFly)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(permit, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftCertPermitsToFly/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var permitInd = _(aircraft.aircraftCertPermitsToFly)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertPermitsToFly.splice(permitInd, 1);

          return [200];
        });
  });
}(angular, _));