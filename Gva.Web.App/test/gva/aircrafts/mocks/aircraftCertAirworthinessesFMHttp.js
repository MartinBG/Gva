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
      .when('GET', 'api/aircrafts/:id/aircraftCertAirworthinessesFM',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertAirworthinessesFM =
            aircraft.aircraftCertAirworthinessesFM.sort(sortByDate);
          return [200, aircraft.aircraftCertAirworthinessesFM];
        })
      .when('GET', 'api/aircrafts/:id/aircraftCertAirworthinessesFM/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var aws = aircraft.aircraftCertAirworthinessesFM.sort(sortByDate),
            aw;
          if ($params.ind && $params.ind !== 'current') {
            aw = _(aws)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            aw = aws.length > 0 ? aws[0] : null;
          }

          if (aw || $params.ind === 'current') {
            return [200, aw];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertAirworthinessesFM',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airworthiness = $jsonData;

          airworthiness.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertAirworthinessesFM.push(airworthiness);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertAirworthinessesFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airworthiness = _(aircraft.aircraftCertAirworthinessesFM)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airworthiness, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftCertAirworthinesses/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var awInd = _(aircraft.aircraftCertAirworthinessesFM)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertAirworthinessesFM.splice(awInd, 1);

          return [200];
        });
  });
}(angular, _));