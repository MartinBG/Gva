/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftDocumentOthers?number&typeid&publ&datef',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number ||
            $params.typeid ||
            $params.publ ||
            $params.datef) {
            var documents = [],
                exists;
            angular.forEach(aircraft.aircraftDocumentOthers, function (document) {
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
            return [200, aircraft.aircraftDocumentOthers];
          }
        })
      .when('GET', '/api/aircrafts/:id/aircraftDocumentOthers/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOther = _(aircraft.aircraftDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentOther) {
            return [200, aircraftDocumentOther];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentOthers',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOther = $jsonData;

          aircraftDocumentOther.partIndex = aircraft.nextIndex++;

          aircraft.aircraftDocumentOthers.push(aircraftDocumentOther);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentOthers/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOther = _(aircraft.aircraftDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentOther, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftDocumentOthers/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOtherInd = _(aircraft.aircraftDocumentOthers)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftDocumentOthers.splice(aircraftDocumentOtherInd, 1);

          return [200];
        });
  });
}(angular, _));