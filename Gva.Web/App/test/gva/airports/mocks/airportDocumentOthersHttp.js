/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/airports/:id/airportDocumentOthers?number&typeid&publ&datef',
        function ($params, $filter, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number ||
            $params.typeid ||
            $params.publ ||
            $params.datef) {
            var documents = [],
                exists;
            angular.forEach(airport.airportDocumentOthers, function (document) {
              if ($params.number) {
                exists = document.part.documentNumber === $params.number;
              }  else if ($params.typeid) {
                var typeId = parseInt($params.typeid, 10);
                exists = document.part.personOtherDocumentType.nomValueId === typeId;
              } else if ($params.publ) {
                exists = document.part.documentPublisher === $params.publ;
              } else if ($params.datef) {
                var newDate = $filter('date')($params.datef, 'mediumDate'),
                  oldDate = $filter('date')(document.part.documentDateValidFrom, 'mediumDate');
                exists = newDate === oldDate;
              }
              if (exists) {
                documents.push(document);
              }
            });
            return [200, documents];
          } else {
            return [200, airport.airportDocumentOthers];
          }
        })
      .when('GET', '/api/airports/:id/airportDocumentOthers/:ind',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOther = _(airport.airportDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (airportDocumentOther) {
            return [200, airportDocumentOther];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/airports/:id/airportDocumentOthers',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOther = $jsonData;

          airportDocumentOther.partIndex = airport.nextIndex++;

          airport.airportDocumentOthers.push(airportDocumentOther);

          return [200];
        })
      .when('POST', '/api/airports/:id/airportDocumentOthers/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOther = _(airport.airportDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airportDocumentOther, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/airports/:id/airportDocumentOthers/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOtherInd = _(airport.airportDocumentOthers)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          airport.airportDocumentOthers.splice(airportDocumentOtherInd, 1);

          return [200];
        });
  });
}(angular, _));