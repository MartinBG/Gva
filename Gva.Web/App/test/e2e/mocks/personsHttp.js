/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function personMapper(p) {
      return {
        id: p.lotId,
        lin: p.personData.part.lin,
        uin: p.personData.part.uin,
        names: p.personData.part.firstName + ' ' +
          p.personData.part.middleName + ' ' + p.personData.part.lastName,
        /*jshint -W052*/
        age: ~~((Date.now() - new Date(p.personData.part.dateOfBirth)) / (31557600000)),
        /*jshint +W052*/
        licences: 'C/AL, ATCL, ATCL, FCL/ATPA, FDA, Part-66 N, FDA, CPL(A), CPL(A)',
        ratings: 'A 300/310, /NAT-OTS MNPS',
        organization: _(p.personDocumentEmployments).pluck('part')
          .where({ valid: true }).pluck('organization').pluck('name').first()
      };
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons?lin&exact',
        function ($params, $filter, personLots) {
          var persons = _(personLots)
            .filter(function (p) {
              if ($params.exact) {
                return p.personData.part.lin === $params.lin;
              } else {
                return p.personData.part.lin.indexOf($params.lin) >= 0;
              }
            })
            .map(personMapper)
            .value();

          return [200, persons];
        })
      .when('GET', '/api/persons/:id',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).map(personMapper).first();

          if (person) {
            return [200, person];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons',
        function ($params, $jsonData, personLots) {
          var nextLotId = _(personLots).pluck('lotId').max().value() + 1;

          personLots.push({
            lotId: nextLotId,
            nextIndex: 4,
            personData: {
              partIndex: 1,
              part: $jsonData.personData
            },
            personAdresses: [
              {
                partIndex: 2,
                part: $jsonData.personAddress
              }
            ],
            personDocumentIds: [
              {
                partIndex: 3,
                part: $jsonData.personDocumentId
              }
            ]
          });

          return [200];
        });
  });
}(angular, _));
