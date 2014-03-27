/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function airportMapper(ac) {
      return {
        id: ac.lotId,
        airportType: ac.airportData.part.airportType,
        name: ac.airportData.part.name,
        nameAlt: ac.airportData.part.nameAlt,
        icao: ac.airportData.part.icao,
        place: ac.airportData.part.place,
        runway: ac.airportData.part.runway,
        course: ac.airportData.part.course,
        excess: ac.airportData.part.excess,
        concrete: ac.airportData.part.concrete
      };
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/airports?name&icao',
        function ($params, $filter, airportLots) {
          var airports = _(airportLots)
          .map(airportMapper)
            .filter(function (p) {
              var isMatch = true;

              _.forOwn($params, function (value, param) {
                if (!value || param === 'exact') {
                  return;
                }

                if ($params.exact) {
                  isMatch = isMatch && p[param] && p[param] === $params[param];
                } else {
                  isMatch = isMatch && p[param] && p[param].toString().indexOf($params[param]) >= 0;
                }

                //short circuit forOwn if not a match
                return isMatch;
              });

              return isMatch;
            })
            .value();

          return [200, airports];
        })
      .when('GET', '/api/airports/:id',
        function ($params, $filter, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).map(airportMapper).first();

          if (airport) {
            return [200, airport];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/airports',
        function ($params, $jsonData, airportLots) {
          var nextLotId = Math.max(_(airportLots).pluck('lotId').max().value() + 1, 1);

          var newAirport = {
            lotId: nextLotId,
            nextIndex: 2,
            airportData: {
              partIndex: 1,
              part: $jsonData.airportData
            },
            airportDocumentOthers: [],
            airportDocumentOwners: [],
            airportCertOperationals: []
          };

          airportLots.push(newAirport);

          return [200, newAirport];
        });
  });
}(angular, _));
