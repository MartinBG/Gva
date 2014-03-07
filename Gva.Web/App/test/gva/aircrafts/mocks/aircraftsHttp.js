/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function aircraftMapper(ac) {
      return {
        id: ac.lotId,
        aircraftProducer: ac.aircraftData.part.aircraftProducer,
        aircraftCategory: ac.aircraftData.part.aircraftCategory,
        CofAType: ac.aircraftData.part.aircraftCategory,
        EASAType: ac.aircraftData.part.aircraftCategory,
        EURegType: ac.aircraftData.part.aircraftCategory,
        EASACategory: ac.aircraftData.part.aircraftCategory,
        icao: ac.aircraftData.part.icao,
        model: ac.aircraftData.part.model,
        modelAlt: ac.aircraftData.part.modelAlt,
        manSN: ac.aircraftData.part.manSN,
        outputDate: ac.aircraftData.part.outputDate,
        engine: ac.aircraftData.part.engine,
        propeller: ac.aircraftData.part.propeller,
        ModifOrWingColor: ac.aircraftData.part.ModifOrWingColor
      };
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts?manSN&model&icao',
        function ($params, $filter, aircraftLots) {
          var aircrafts = _(aircraftLots)
          .map(aircraftMapper)
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

          return [200, aircrafts];
        })
      .when('GET', '/api/aircrafts/:id',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).map(aircraftMapper).first();

          if (aircraft) {
            return [200, aircraft];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts',
        function ($params, $jsonData, aircraftLots) {
          var nextLotId = Math.max(_(aircraftLots).pluck('lotId').max().value() + 1, 1);

          var newAircraft = {
            lotId: nextLotId,
            nextIndex: 2,
            aircraftData: {
              partIndex: 1,
              part: $jsonData.aircraftData
            },
            aircraftCertRegistrations: [],
            aircraftCertSmods: [],
            aircraftCertMarks: [],
            aircraftCertAirworthinesses: [],
            aircraftCertNoises: [],
            aircraftCertPermitsToFly: [],
            aircraftCertRadios: []
          };

          aircraftLots.push(newAircraft);

          return [200, newAircraft];
        });
  });
}(angular, _));
