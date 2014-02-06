/*global angular, require, _*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      corrs = [{
        corrId: 1,
        correspondentType: nomenclatures.correspondentTypes[0],
        correspondentGroup: nomenclatures.correspondentGroups[0],
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
        correspondentType: nomenclatures.correspondentTypes[1],
        correspondentGroup: nomenclatures.correspondentGroups[1],
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
      .when('GET', '/api/nomenclatures/corrs',
        function () {
          var noms = [],
            nomItem = {
              nomTypeValueId: '',
              name: '',
              content: []
            };

          _.forEach(corrs, function (item) {
            var t = {};

            nomItem.nomTypeValueId = item.corrId;
            nomItem.name = item.displayName;
            nomItem.content = item;

            _.assign(t, nomItem, true);
            noms.push(t);
          });

          return [200, noms];
        }
      )
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

          if ($jsonData.correspondentType.nomTypeValueId === 1) {
            $jsonData.displayName = $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
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

          if ($jsonData.correspondentType.nomTypeValueId === 1) {
            $jsonData.displayName = $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }
          else if ($jsonData.correspondentType.nomTypeValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }

          corrs[corrIndex] = $jsonData;

          return [200];
        });
  });
}(angular, _));
