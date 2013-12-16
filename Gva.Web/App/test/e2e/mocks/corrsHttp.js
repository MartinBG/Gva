/*global angular, require*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      corrs = [{
        corrId: 1,
        corrType: {
          nomTypeValueId: nomenclatures.getId('CorrespondentTypes', 'Foreigner'),
          name: nomenclatures.getName('CorrespondentTypes', 'Foreigner')
        },
        corrGroup: {
          nomTypeValueId: nomenclatures.getId('CorrespondentGroups', 'Applicants'),
          name: nomenclatures.getName('CorrespondentGroups', 'Applicants')
        },
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
        corrType: {
          corrTypeId: nomenclatures.getId('CorrespondentTypes', 'Foreigner'),
          name: nomenclatures.getName('CorrespondentTypes', 'Foreigner')
        },
        corrGroup: {
          corrGroupId: nomenclatures.getId('CorrespondentGroups', 'Applicants'),
          name: nomenclatures.getName('CorrespondentGroups', 'Applicants')
        },
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
      .when('GET', '/api/corrs?corrUin&corrEmail',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(corrs, {
              corrUin: $params.corrUin,
              corrEmail: $params.corrEmail
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

          //$jsonData.hasPassword = $jsonData.password !== undefined && $jsonData.password !== '';
          corrs[corrIndex] = $jsonData;

          return [200];
        })
      .when('POST', '/api/corrs',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.corrId) {
            return [400];
          }

          $jsonData.corrId = ++nextCorrId;
          //$jsonData.hasPassword = $jsonData.password !== undefined && $jsonData.password !== '';
          corrs.push($jsonData);

          return [200];
        });
  });
}(angular));
