/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Видове(типове) правоспособност
  module.exports = [
    {
      nomTypeValueId: 7356, code: 'PPL(A)', name: 'Любител пилот на самолет (PPL(A))', nameAlt: 'Private Pilot  (Aeroplane) (PPL(A))', nomTypeParentValueId: 5591, alias: 'PPLA',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 1,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'PPL(A)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7357, code: 'CPL(A)', name: 'Професионален пилот на самолет CPL(A)', nameAlt: 'Commercial Pilot  (Aeroplane) (CPL(A))', nomTypeParentValueId: 5591, alias: 'CPL',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 2,
        prtMaxRatingCount: 14,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'CPL(A)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7358, code: 'ATPL(A)', name: 'Транспортен пилот на самолет ATPL(A)', nameAlt: 'Airline transport Pilot (Aeroplane) (ATPL(A))', nomTypeParentValueId: 5591, alias: 'ATPL',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 3,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'ATPL(A)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7359, code: 'PPL(H)', name: 'Любител пилот на вертолет (PPL(H))', nameAlt: 'Private Pilot  (Helicopter) (PPL(H))', nomTypeParentValueId: 5591, alias: 'PPLH',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 4,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'PPL(H)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7360, code: 'CPL(H)', name: 'Професионален пилот на вертолет (CPL(H))', nameAlt: 'Commercial Pilot  (CPL(H))', nomTypeParentValueId: 5591, alias: 'CPL(H)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 5,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'CPL(H)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7361, code: 'ATPL(H)', name: 'Транспортен пилот на вертолет (ATPL(H))', nameAlt: 'Airline Transport Pilot  (Helocopter) (ATPL(H))', nomTypeParentValueId: 5591, alias: 'ATPL(H)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 6,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'ATPL(H)',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7362, code: 'PL(G)', name: 'Пилот на планер (PL(G))', nameAlt: 'Pilot  (Glider)  (PL(G))', nomTypeParentValueId: 5591, alias: 'PL(G)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 7,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'PLG',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7363, code: 'PL(FB)', name: 'Пилот на свободен балон (PL(FB))', nameAlt: 'Pilot  (Free baloons) (PL(FB))', nomTypeParentValueId: 5591, alias: 'PL(FB)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 8,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'PFB',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7364, code: 'PPL(SA)', name: 'Любител пилот на малки въздухоплавателни средства PPL(SA)', nameAlt: 'Private Pilot  (Small Aircraft) (PPL(SA))', nomTypeParentValueId: 5591, alias: 'PPL(SA)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 9,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7334,
        licenceCode: 'PPSA',
        qlfCode: null }
    },
    {
      nomTypeValueId: 7346, code: 'FDA', name: 'Асистент - координатор на полети ', nameAlt: 'Flight data assistant', nomTypeParentValueId: '5592', alias: 'FDA',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 13,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7332,
        licenceCode: 'FDAL',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7347, code: 'ATCL', name: 'Ръководител полети ', nameAlt: null, nomTypeParentValueId: 5592, alias: 'ATCL',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 15,
        prtMaxRatingCount: 9,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7329,
        licenceTypeDictionary1Id: 7330,
        licenceTypeDictionary2Id: 7328,
        licenceCode: 'ATCL',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7348, code: 'CATML', name: 'Координатор по УВД ', nameAlt: null, nomTypeParentValueId: 5592, alias: 'CATML',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 14,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7332,
        licenceCode: 'CATML',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7349, code: 'SATCL', name: 'Ученик ръководител полети', nameAlt: null, nomTypeParentValueId: 5592, alias: 'SATCL',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 16,
        prtMaxRatingCount: 11,
        prtMaxMedCertCount: 1,
        licenceTypeDictionaryId: 7331,
        licenceCode: 'SATCL',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7351, code: 'AML', name: 'Свидетелство за правоспособност за ТО на ВС-национален', nameAlt: null, nomTypeParentValueId: 5590, alias: 'AML',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 18,
        prtMaxRatingCount: 10,
        prtMaxMedCertCount: 1,
        licenceCode: 'AM',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7353, code: 'TO(AML)', name: 'Техническо обслужване на ВС', nameAlt: null, nomTypeParentValueId: 5590, alias: 'TO(AML)',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 20,
        prtMaxRatingCount: 30,
        prtMaxMedCertCount: 0,
        licenceCode: '66',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7373, code: 'Part-66 N', name: 'Техническо обслужване на ВС-new', nameAlt: null, nomTypeParentValueId: 5590, alias: 'Part-66 N',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 21,
        prtMaxRatingCount: 30,
        prtMaxMedCertCount: 0,
        licenceCode: '66.A',
        qlfCode: null
      }
    },
    {
      nomTypeValueId: 7374, code: 'ATSML', name: 'Техническо обслужване на средствата за РВД', nameAlt: null, nomTypeParentValueId: 5589, alias: 'ATSML',
      content: {
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z',
        seqNo: 32,
        prtMaxRatingCount: 30,
        prtMaxMedCertCount: 0,
        licenceCode: 'ATSML',
        qlfCode: null }
    }
  ];
})(typeof module === 'undefined' ? (this['licenceType'] = {}) : module);
