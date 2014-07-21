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
      .when('GET', 'api/aircrafts/:id/aircraftCertRadios',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertRadios =
            aircraft.aircraftCertRadios.sort(sortByDate);
          return [200, aircraft.aircraftCertRadios];
        })
      .when('GET', 'api/aircrafts/:id/aircraftCertRadios/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var radios = aircraft.aircraftCertRadios.sort(sortByDate),
            radio;
          if ($params.ind && $params.ind !== 'current') {
            radio = _(radios)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            radio = radios.length > 0 ? radios[0] : null;
          }

          if (radio || $params.ind === 'current') {
            return [200, radio];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertRadios',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var radio = $jsonData;

          radio.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertRadios.push(radio);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftCertRadios/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var radio = _(aircraft.aircraftCertRadios)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(radio, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftCertRadios/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var radioInd = _(aircraft.aircraftCertRadios)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertRadios.splice(radioInd, 1);

          return [200];
        });
  });
}(angular, _));