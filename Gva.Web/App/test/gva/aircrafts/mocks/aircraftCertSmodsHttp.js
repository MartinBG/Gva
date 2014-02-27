/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function sortByDate(a, b) {
      if (a.part.ltrInDate > b.part.ltrInDate) {
        return -1;
      } else if (a.part.ltrInDate < b.part.ltrInDate) {
        return 1;
      } else {
        return 0;
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftCertSmods',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertSmods =
            aircraft.aircraftCertSmods.sort(sortByDate);
          return [200, aircraft.aircraftCertSmods];
        })
      .when('GET', '/api/aircrafts/:id/aircraftCertSmods/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var smods = aircraft.aircraftCertSmods.sort(sortByDate),
            smod;
          if ($params.ind && $params.ind !== 'current') {
            smod = _(smods)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            smod = smods.length > 0 ? smods[0] : null;
          }

          if (smod || $params.ind === 'current') {
            return [200, smod];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertSmods',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var smod = $jsonData;

          smod.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertSmods.push(smod);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertSmods/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var smod = _(aircraft.aircraftCertSmods)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(smod, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftCertSmods/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var smodInd = _(aircraft.aircraftCertSmods)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertSmods.splice(smodInd, 1);

          return [200];
        });
  });
}(angular, _));