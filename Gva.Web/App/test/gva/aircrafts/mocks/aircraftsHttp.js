/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function aircraftMapper(ac) {
      return {
        id: ac.lotId,
        aircraftProducer: ac.aircraftData.part.aircraftProducer,
        aircraftCategory: ac.aircraftData.part.aircraftCategory,
        icao: ac.aircraftData.part.icao,
        modelName: ac.aircraftData.part.name + ' ' +
          ac.aircraftData.part.series + ' ' +
          ac.aircraftData.part.model,
        modelNameAlt: ac.aircraftData.part.nameAlt + ' ' +
          ac.aircraftData.part.series + ' ' +
          ac.aircraftData.part.model,
        manSN: ac.aircraftData.part.manSN
      };
    }

    $httpBackendConfiguratorProvider
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
        });
  });
}(angular, _));
