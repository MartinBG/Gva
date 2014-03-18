/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function sortByDate(a, b) {
      if (a.part.issueDate > b.part.issueDate) {
        return -1;
      } else if (a.part.issueDate < b.part.issueDate) {
        return 1;
      } else {
        return 0;
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftCertNoisesFM',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertNoisesFM =
            aircraft.aircraftCertNoisesFM.sort(sortByDate);
          return [200, aircraft.aircraftCertNoisesFM];
        })
      .when('GET', '/api/aircrafts/:id/aircraftCertNoisesFM/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var noises = aircraft.aircraftCertNoisesFM.sort(sortByDate),
            noise;
          if ($params.ind && $params.ind !== 'current') {
            noise = _(noises)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            noise = noises.length > 0 ? noises[0] : null;
          }

          if (noise || $params.ind === 'current') {
            return [200, noise];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertNoisesFM',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var noise = $jsonData;

          noise.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertNoisesFM.push(noise);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertNoisesFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var noise = _(aircraft.aircraftCertNoisesFM)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(noise, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftCertNoisesFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var noiseInd = _(aircraft.aircraftCertNoisesFM)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertNoisesFM.splice(noiseInd, 1);

          return [200];
        });
  });
}(angular, _));