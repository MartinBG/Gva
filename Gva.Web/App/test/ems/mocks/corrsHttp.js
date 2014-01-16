/*global angular, require*/
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
        displayName: 'ДЕЛТА КОИН 1324567890',
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

          $jsonData.corrId = ++nextCorrId;
          corrs.push($jsonData);

          return [200];
        })
      .when('GET', '/api/corrs/new',
        function () {
          var newCorr = {
            corrId: undefined,
            correspondentType: undefined,
            correspondentGroup: undefined,
            displayName: undefined,
            email: undefined,
            bgCitizenFirstName: undefined,
            bgCitizenLastName: undefined,
            bgCitizenUIN: undefined,

            foreignerFirstName: undefined,
            foreignerLastName: undefined,
            foreignerCountryId: undefined,
            foreignerSettlement: undefined,
            foreignerBirthDate: undefined,

            LegalEntityName: undefined,
            LegalEntityBulstat: undefined,

            fLegalEntityName: undefined,
            fLegalEntityCountryId: undefined,
            fLegalEntityRegisterName: undefined,
            fLegalEntityRegisterNumber: undefined,
            fLegalEntityOtherData: undefined,

            contactDistrictId: undefined,
            contactMunicipalityId: undefined,
            contactSettlementId: undefined,
            contactPostCode: undefined,
            contactAddress: undefined,
            contactPostOfficeBox: undefined,
            contactPhone: undefined,
            contactFax: undefined,
            correspondentContacts: []
          };

          return [200, newCorr];
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

          corrs[corrIndex] = $jsonData;

          return [200];
        })
      .when('GET', '/api/corrs/contacts/new/:corrId',
        function ($params) {
          var corrId = parseInt($params.corrId, 10),
            correspondentContact = {
              correspondentContactId: nextCorrContactId++,
              corrId: corrId,
              name: undefined,
              uin: undefined,
              note: undefined,
              isActive: true
            };

          return [200, correspondentContact];
        });
  });
}(angular));
