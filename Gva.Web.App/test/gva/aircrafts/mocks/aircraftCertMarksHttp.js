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
      .when('GET', '/api/aircrafts/:id/aircraftCertMarks',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          aircraft.aircraftCertMarks =
            aircraft.aircraftCertMarks.sort(sortByDate);
          return [200, aircraft.aircraftCertMarks];
        })
      .when('GET', '/api/aircrafts/:id/aircraftCertMarks/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var marks = aircraft.aircraftCertMarks.sort(sortByDate),
            mark;
          if ($params.ind && $params.ind !== 'current') {
            mark = _(marks)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();
          } else {
            mark = marks.length > 0 ? marks[0] : null;
          }

          if (mark || $params.ind === 'current') {
            return [200, mark];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertMarks',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var mark = $jsonData;

          mark.partIndex = aircraft.nextIndex++;

          aircraft.aircraftCertMarks.push(mark);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftCertMarks/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var mark = _(aircraft.aircraftCertMarks)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(mark, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftCertMarks/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var markInd = _(aircraft.aircraftCertMarks)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftCertMarks.splice(markInd, 1);

          return [200];
        });
  });
}(angular, _));