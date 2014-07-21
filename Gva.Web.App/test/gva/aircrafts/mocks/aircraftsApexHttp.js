/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function aircraftMapper(ac) {
      return {
        id: ac.lotId,
        aircraftProducer: ac.aircraftDataApex.part.aircraftProducer,
        aircraftCategory: ac.aircraftDataApex.part.aircraftCategory,
        CofAType: ac.aircraftDataApex.part.aircraftCategory,
        EASAType: ac.aircraftDataApex.part.aircraftCategory,
        EURegType: ac.aircraftDataApex.part.aircraftCategory,
        EASACategory: ac.aircraftDataApex.part.aircraftCategory,
        icao: ac.aircraftDataApex.part.icao,
        model: ac.aircraftDataApex.part.model,
        modelAlt: ac.aircraftDataApex.part.modelAlt,
        manSN: ac.aircraftDataApex.part.manSN,
        outputDate: ac.aircraftDataApex.part.outputDate,
        engine: ac.aircraftDataApex.part.engine,
        propeller: ac.aircraftDataApex.part.propeller,
        ModifOrWingColor: ac.aircraftDataApex.part.ModifOrWingColor
      };
    }

    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircraftsApex?manSN&model&icao',
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
      .when('GET', 'api/aircraftsApex/:id',
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
      .when('POST', 'api/aircraftsApex',
        function ($params, $jsonData, aircraftLots) {
          var nextLotId = Math.max(_(aircraftLots).pluck('lotId').max().value() + 1, 1);

          var newAircraft = {
            lotId: nextLotId,
            nextIndex: 2,
            aircraftDataApex: {
              partIndex: 1,
              part: $jsonData.aircraftDataApex
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
