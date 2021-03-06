﻿/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Reg1: {
      isActive: nomenclatures.get('boolean', 'false'),
      register: nomenclatures.get('registers', 'R1'),
      certNumber: 1877,
      certDate: '1999-05-15T00:00',
      regMark: 'LZ-ACD',
      incomingDocNumber: '',
      incomingDocDate: '1999-05-15T00:00',
      incomingDocDesc: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      owner: nomenclatures.get('organizations', 'AAK Progres'),
      operator: nomenclatures.get('organizations', 'AAK Progres'),
      category: nomenclatures.get('aircraftCategories', 'A2'),
      limitation: nomenclatures.get('aircraftLimitations', 'L3'),
      leasingDocNumber: 'l12-a2',
      leasingDocDate: '1999-05-15T00:00',
      leasingLessor: '',
      leasingAgreement: '',
      leasingEndDate: '1999-05-15T00:00',
      status: nomenclatures.get('aircraftRegStatuses', 'R3'),
      EASA25Number: '',
      EASA25Date: '1999-05-15T00:00',
      EASA15Date: '1999-05-15T00:00',
      cofRDate: '1999-05-15T00:00',
      noiseDate: '1999-05-15T00:00',
      noiseNumber: 'N-12',
      paragraph: '',
      paragraphAlt: '',
      removalDate: '2003-05-15T00:00',
      removalReason: 'промяна',
      removalText: '',
      removalDocumentNumber: 'МИГРАЦИЯ 15.05.1999 - служебно присвоена дата на анулиране',
      removalDocumentDate: '2003-05-15T00:00',
      removalInspector: nomenclatures.get('inspectors', 'Vladimir')
    },
    aircraft1Reg2: {
      isActive: nomenclatures.get('boolean', 'false'),
      register: nomenclatures.get('registers', 'R1'),
      certNumber: 2022,
      certDate: '2001-05-15T00:00',
      regMark: 'LZ-YUK',
      incomingDocNumber: '',
      incomingDocDate: '2001-05-15T00:00',
      incomingDocDesc: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      owner: nomenclatures.get('organizations', 'Wizz Air'),
      operator: nomenclatures.get('organizations', 'AAK Progres'),
      category: nomenclatures.get('aircraftCategories', 'A2'),
      limitation: nomenclatures.get('aircraftLimitations', 'L3'),
      leasingDocNumber: 'l12-a3',
      leasingDocDate: '2001-05-15T00:00',
      leasingLessor: '',
      leasingAgreement: '',
      leasingEndDate: '2001-05-15T00:00',
      status: nomenclatures.get('aircraftRegStatuses', 'R3'),
      EASA25Number: '',
      EASA25Date: '2001-05-15T00:00',
      EASA15Date: '2001-05-15T00:00',
      cofRDate: '2001-05-15T00:00',
      noiseDate: '2001-05-15T00:00',
      noiseNumber: 'N-122',
      paragraph: '',
      paragraphAlt: '',
      removalDate: '2005-05-15T00:00',
      removalReason: 'промяна',
      removalText: '',
      removalDocumentNumber: 'МИГРАЦИЯ 15.05.2001 - служебно присвоена дата на анулиране',
      removalDocumentDate: '2005-05-15T00:00',
      removalInspector: nomenclatures.get('inspectors', 'Vladimir')
    },
    aircraft1Reg3: {
      isActive: nomenclatures.get('boolean', 'false'),
      register: nomenclatures.get('registers', 'R1'),
      certNumber: 2151,
      certDate: '2003-05-15T00:00',
      regMark: 'LZ-FED',
      incomingDocNumber: '',
      incomingDocDate: '2003-05-15T00:00',
      incomingDocDesc: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      owner: nomenclatures.get('organizations', 'AAK Progres'),
      operator: nomenclatures.get('organizations', 'Wizz Air'),
      category: nomenclatures.get('aircraftCategories', 'A2'),
      limitation: nomenclatures.get('aircraftLimitations', 'L3'),
      leasingDocNumber: 'l12-a22',
      leasingDocDate: '2003-05-15T00:00',
      leasingLessor: '  ',
      leasingAgreement: '',
      leasingEndDate: '2003-05-15T00:00',
      status: nomenclatures.get('aircraftRegStatuses', 'R3'),
      EASA25Number: '',
      EASA25Date: '2003-05-15T00:00',
      EASA15Date: '2003-05-15T00:00',
      cofRDate: '2003-05-15T00:00',
      noiseDate: '2003-05-15T00:00',
      noiseNumber: 'N-as12',
      paragraph: '',
      paragraphAlt: '',
      removalDate: '2003-05-15T00:00',
      removalReason: 'промяна',
      removalText: '',
      removalDocumentNumber: 'МИГРАЦИЯ 15.05.2003 - служебно присвоена дата на анулиране',
      removalDocumentDate: '2003-05-15T00:00',
      removalInspector: nomenclatures.get('inspectors', 'Vladimir')
    },
    aircraft1Reg4: {
      isActive: nomenclatures.get('boolean', 'true'),
      register: nomenclatures.get('registers', 'R1'),
      certNumber: 2231,
      certDate: '2005-05-15T00:00',
      regMark: 'LZ-FED',
      incomingDocNumber: '',
      incomingDocDate: '2005-05-15T00:00',
      incomingDocDesc: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      owner: nomenclatures.get('organizations', 'Wizz Air'),
      operator: nomenclatures.get('organizations', 'Wizz Air'),
      category: nomenclatures.get('aircraftCategories', 'A2'),
      limitation: nomenclatures.get('aircraftLimitations', 'L3'),
      leasingDocNumber: 'l12-a22',
      leasingDocDate: '2005-05-15T00:00',
      leasingLessor: '',
      leasingAgreement: '',
      leasingEndDate: '2005-05-15T00:00',
      status: nomenclatures.get('aircraftRegStatuses', 'R3'),
      EASA25Number: '',
      EASA25Date: '2005-05-15T00:00',
      EASA15Date: '2005-05-15T00:00',
      cofRDate: '2005-05-15T00:00',
      noiseDate: '2005-05-15T00:00',
      noiseNumber: 'N-12ac.2',
      paragraph: '',
      paragraphAlt: '',
      removalDate: '2013-05-15T00:00',
      removalReason: 'промяна',
      removalText: '',
      removalDocumentNumber: 'МИГРАЦИЯ 15.05.2013 - служебно присвоена дата на анулиране',
      removalDocumentDate: '2013-05-15T00:00',
      removalInspector: nomenclatures.get('inspectors', 'Vladimir')
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-registrationsfm.sample'] = {}) : module);
