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
        foreignerCountryId: '',
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
        contactFax: ''
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
        contactFax: ''
      }],
      nextCorrId = 3;

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
      .when('POST', '/api/corrs',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.corrId) {
            return [400];
          }

          $jsonData.corrId = ++nextCorrId;
          corrs.push($jsonData);

          return [200];
        });
  });
}(angular));
