/*global angular, require, _*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      corrs = [{
        correspondentId: 1,
        correspondentTypeId: 1,
        correspondentType: nomenclatures.correspondentType[0],
        correspondentGroupId: 2,
        correspondentGroup: nomenclatures.correspondentGroup[1],
        displayName: 'ДЕЛТА КОИН 1324567890',
        email: 'delta@coin.com',
        bgCitizenFirstName: 'ДЕЛТА',
        bgCitizenLastName: 'КОИН',
        bgCitizenUIN: '1324567890',

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
          correspondentId: 1,
          name: 'Tsvetan Belchev',
          uin: '8903269357',
          note: 'keine',
          isActive: true,
          isNew: false,
          isDirty: false,
          isDeleted: false,
          isInEdit: false
        }, {
          correspondentContactId: 2,
          correspondentId: 1,
          name: 'Belchev Tsvetan',
          uin: '8903269357',
          note: 'eine',
          isActive: true,
          isNew: false,
          isDirty: false,
          isDeleted: false,
          isInEdit: false
        }]
      }, {
        correspondentId: 2,
        correspondentTypeId: 2,
        correspondentType: nomenclatures.correspondentType[1],
        correspondentGroupId: 2,
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

        LegalEntityName: 'АЛИ БАБА',
        LegalEntityBulstat: '4040404040',

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
      nextCorrespondentId = 3,
      nextCorrespondentContactId = 3;

    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/corrs?id',
        function ($params, $filter) {

          var res = _(corrs).map(function (item) {
            return {
              nomValueId: item.correspondentId,
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


      .when('GET', '/api/corrs/new',
        function () {
          return [200, {}];
        })
      .when('GET', '/api/corrs?displayName&email&limit&offset',
        function ($params, $filter) {
          var correspondents = $filter('filter')(corrs, {
            displayName: $params.displayName,
            email: $params.email
          });
          return [
            200,
            {
              correspondents: correspondents,
              correspondentCount: correspondents.length
            }
          ];
        })
       .when('POST', '/api/corrs',
        function ($params, $jsonData) {
          if (!$jsonData || $jsonData.correspondentId) {
            return [400];
          }

          if ($jsonData.correspondentType.nomValueId === 1) {
            $jsonData.displayName =
              $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName + ' ' +
              $jsonData.bgCitizenUIN;
          } else if ($jsonData.correspondentType.nomValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          } else if ($jsonData.correspondentType.nomValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          } else if ($jsonData.correspondentType.nomValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName;
          }

          $jsonData.correspondentId = ++nextCorrespondentId;
          corrs.push($jsonData);

          return [200];
        })
      .when('GET', '/api/corrs/:id',
        function ($params, $filter) {
          var correspondentId = parseInt($params.id, 10),
            corr = $filter('filter')(corrs, { correspondentId: correspondentId })[0];

          if (!corr) {
            return [400];
          }

          _.forEach(corr.correspondentContacts, function (item) {
            item.isInEdit = false;
          });

          return [200, corr];
        })
      .when('POST', '/api/corrs/:id',
        function ($params, $jsonData, $filter) {
          var correspondentId = parseInt($params.id, 10),
            corrIndex = corrs.indexOf($filter('filter')(
              corrs,
              { correspondentId: correspondentId })[0]
            );

          if (corrIndex === -1) {
            return [400];
          }

          _.remove($jsonData.correspondentContacts, function (item) {
            if (item.isDeleted) {
              return true;
            }

            return false;
          });

          _.forEach($jsonData.correspondentContacts, function (item) {
            if (!item.correspondentContactId) {
              item.correspondentContactId = nextCorrespondentContactId++;
            }
          });

          if ($jsonData.correspondentType.nomValueId === 1) {
            $jsonData.displayName =
              $jsonData.bgCitizenFirstName + ' ' +
              $jsonData.bgCitizenLastName + ' ' +
              $jsonData.bgCitizenUIN;
          } else if ($jsonData.correspondentType.nomValueId === 2) {
            $jsonData.displayName = $jsonData.foreignerFirstName + ' ' +
              $jsonData.foreignerLastName;
          } else if ($jsonData.correspondentType.nomValueId === 3) {
            $jsonData.displayName = $jsonData.legalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          } else if ($jsonData.correspondentType.nomValueId === 4) {
            $jsonData.displayName = $jsonData.fLegalEntityName + ' ' +
              $jsonData.legalEntityBulstat;
          }

          corrs[corrIndex] = $jsonData;

          return [200];
        });
  });
}(angular, _));
