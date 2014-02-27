/*global angular, require, _*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      corrs = [{
        corrId: 1,
        correspondentType: nomenclatures.correspondentType[0],
        correspondentGroup: nomenclatures.correspondentGroup[0],
        displayName: 'ДЕЛТА КОИН 1324567890',
        email: 'delta@coin.com',
        bgCitizenFirstName: '',
        bgCitizenLastName: '',
        bgCitizenUIN: '',

        foreignerFirstName: '',
        foreignerLastName: '',
        foreignerBirthDate: '',
        foreignerCountry: nomenclatures.get('countries', 'Belgium'),
        foreignerSettlement: '',

        legalEntityName: '',
        legalEntityBulstat: '',

        fLegalEntityName: '',
        fLegalEntityCountryId: '',
        fLegalEntityRegisterName: '',
        fLegalEntityRegisterNumber: '',
        fLegalEntityOtherData: '',

        contactDistrictId: '',
        contactMunicipalityId: '',
        contactSettlementId: '',
        contactPostCode: '',
        contactAddress: '',
        contactPostOfficeBox: '',
        contactPhone: '',
        contactFax: '',
        correspondentContacts: [{
          correspondentContactId: 1,
          corrId: 1,
          name: 'Tsvetan Belchev',
          uin: '8903269357',
          note: 'keine',
          isActive: true
        }, {
          correspondentContactId: 2,
          corrId: 1,
          name: 'Belchev Tsvetan',
          uin: '8903269357',
          note: 'eine',
          isActive: true
        }]
      }, {
        corrId: 2,
        correspondentType: nomenclatures.correspondentType[1],
        correspondentGroup: nomenclatures.correspondentGroup[1],
        displayName: 'АЛИ БАБА 4040404040',
        email: 'delta@coin.com',
        bgCitizenFirstName: '',
        bgCitizenLastName: '',
        bgCitizenUIN: '',

        foreignerFirstName: '',
        foreignerLastName: '',
        foreignerCountryId: '',
        foreignerSettlement: '',
        foreignerBirthDate: '',

        LegalEntityName: '',
        LegalEntityBulstat: '',

        fLegalEntityName: '',
        fLegalEntityCountryId: '',
        fLegalEntityRegisterName: '',
        fLegalEntityRegisterNumber: '',
        fLegalEntityOtherData: '',

        contactDistrictId: '',
        contactMunicipalityId: '',
        contactSettlementId: '',
        contactPostCode: '',
        contactAddress: '',
        contactPostOfficeBox: '',
        contactPhone: '',
        contactFax: '',
        correspondentContacts: []

      }],
      nextCorrId = 3,
      nextCorrContactId = 3;

    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/corrs?id',
       function ($params, $filter) {

        var res = _(corrs).map(function (item) {
          return {
            nomValueId: item.corrId,
            name: item.displayName
          };
        }).value();

        if ($params.id) {
          res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
        }

        return [200, res];
      })
      .when('GET', '/api/nomenclatures/persons?id',
        function ($params, $filter, personLots) {

          var res = _(personLots).map(function (item) {
            return {
              nomValueId: item.lotId,
              name: item.personData.part.firstName + ' ' + item.personData.part.lastName
            };
          }).value();

          if ($params.id) {
            res = $filter('filter')(res, { nomValueId: parseInt($params.id, 10) }, true)[0];
          }

          return [200, res];
        })


      .when('GET', '/api/corrs?displayName&email',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(corrs, {
              displayName: $params.displayName,
              email: $params.email
            })
          ];
        })
       .when('POST', '/api/corrs',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.corrId) {
            return [400];
          }

          if ($jsonData.correspondentType.nomValueId === 1) {
            $jsonData.displayName = $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName;
          }
          else if ($jsonData.correspondentType.nomValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          }
          else if ($jsonData.correspondentType.nomValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }
          else if ($jsonData.correspondentType.nomValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName;
          }

          $jsonData.corrId = ++nextCorrId;
          corrs.push($jsonData);

          return [200];
        })
      .when('GET', '/api/corrs/:corrId',
        function ($params, $filter) {
          var corrId = parseInt($params.corrId, 10),
            corr = $filter('filter')(corrs, { corrId: corrId })[0];

          if (!corr) {
            return [400];
          }

          return [200, corr];
        })
      .when('POST', '/api/corrs/:corrId',
        function ($params, $jsonData, $filter) {
          var corrId = parseInt($params.corrId, 10),
            corrIndex = corrs.indexOf($filter('filter')(corrs, { corrId: corrId })[0]);

          if (corrIndex === -1) {
            return [400];
          }

          _.forEach($jsonData.correspondentContacts, function (item) {
            if (!item.correspondentContactId) {
              item.correspondentContactId = nextCorrContactId++;
            }
          });

          if ($jsonData.correspondentType.nomValueId === 1) {
            $jsonData.displayName = $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName;
          }
          else if ($jsonData.correspondentType.nomValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          }
          else if ($jsonData.correspondentType.nomValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }
          else if ($jsonData.correspondentType.nomValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }

          corrs[corrIndex] = $jsonData;

          return [200];
        });
  });
}(angular, _));
