/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Reg1: {
      register: nomenclatures.get('registers', 'R1'),
      owner: nomenclatures.get('organizations', 'AAK Progres'),
      oper: nomenclatures.get('organizations', 'AAK Progres'),
      aircraftCertificateType: nomenclatures.get('aircraftCertificateTypes', 'CT1'),
      certNumber: 1877,
      certDate: '1999-05-15T00:00',
      regMark: 'LZ-FED',
      aircraftPartStatus: 'Проверяващ',
      regNotes: 'Съдебно решение за смяна името на собственика',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      paragraph: '',
      paragraphAlt: '',
      aircraftNewOld: nomenclatures.get('aircraftNewOld', 'new'),
      isActive: nomenclatures.get('boolean', 'false'),
      removal: {
        date: '2003-05-15T00:00',
        reason: 'промяна',
        documentNumber: 'МИГРАЦИЯ 15.05.2003 - служебно присвоена дата на анулиране',
        documentDate: '2003-05-15T00:00'
      },
      typeCert: {
        aircraftTypeCertificateType: nomenclatures.get('aircraftTypeCertificateTypes', 'TCT1'),
        certNumber: 'EASA.IM.A.277',
        certDate: null,
        certRelease: null,
        contry: null
      }
    },
    aircraft1Reg2: {
      register: nomenclatures.get('registers', 'R1'),
      owner: nomenclatures.get('organizations', 'Wizz Air'),
      oper: nomenclatures.get('organizations', 'Wizz Air'),
      aircraftCertificateType: nomenclatures.get('aircraftCertificateTypes', 'CT2'),
      certNumber: 2022,
      certDate: '2003-05-15T00:00',
      regMark: 'LZ-FED',
      aircraftPartStatus: 'Проверяващ',
      regNotes: 'Договор за оперативен лизинг; Заповед R-83-78/21.11.2005',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      paragraph: '',
      paragraphAlt: '',
      aircraftNewOld: nomenclatures.get('aircraftNewOld', 'new'),
      isActive: nomenclatures.get('boolean', 'false'),
      removal: {
        date: '2007-05-15T00:00',
        reason: 'промяна',
        documentNumber: 'МИГРАЦИЯ 15.05.2007 - служебно присвоена дата на анулиране',
        documentDate: '2007-05-15T00:00'
      },
      typeCert: {
        aircraftTypeCertificateType: nomenclatures.get('aircraftTypeCertificateTypes', 'TCT1'),
        certNumber: 'EASA.IM.A.277',
        certDate: null,
        certRelease: null,
        contry: null
      }
    },
    aircraft1Reg3: {
      register: nomenclatures.get('registers', 'R1'),
      owner: nomenclatures.get('organizations', 'Fly Emirates'),
      oper: nomenclatures.get('organizations', 'Fly Emirates'),
      aircraftCertificateType: nomenclatures.get('aircraftCertificateTypes', 'CT1'),
      certNumber: 2151,
      certDate: '2007-05-15T00:00',
      regMark: 'LZ-YUK',
      aircraftPartStatus: 'Проверяващ',
      regNotes: 'В.Текнеджиев',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      paragraph: '',
      paragraphAlt: '',
      aircraftNewOld: nomenclatures.get('aircraftNewOld', 'new'),
      isActive: nomenclatures.get('boolean', 'false'),
      removal: {
        date: '2013-05-15T00:00',
        reason: 'промяна',
        documentNumber: 'МИГРАЦИЯ 15.05.2013 - служебно присвоена дата на анулиране',
        documentDate: '2013-05-15T00:00'
      },
      typeCert: {
        aircraftTypeCertificateType: nomenclatures.get('aircraftTypeCertificateTypes', 'TCT1'),
        certNumber: 'EASA.IM.A.277',
        certDate: null,
        certRelease: null,
        contry: null
      }
    },
    aircraft1Reg4: {
      register: nomenclatures.get('registers', 'R1'),
      owner: nomenclatures.get('organizations', 'AAK Progres'),
      oper: nomenclatures.get('organizations', 'AAK Progres'),
      aircraftCertificateType: nomenclatures.get('aircraftCertificateTypes', 'CT1'),
      certNumber: 2231,
      certDate: '2013-05-15T00:00',
      regMark: 'LZ-YUK',
      aircraftPartStatus: 'Проверяващ',
      regNotes: 'Промяна адрес на собственик В.Текнеджиев',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      paragraph: '',
      paragraphAlt: '',
      aircraftNewOld: nomenclatures.get('aircraftNewOld', 'new'),
      isActive: nomenclatures.get('boolean', 'true'),
      removal: {
        date: null,
        reason: null,
        documentNumber: null,
        documentDate: null
      },
      typeCert: {
        aircraftTypeCertificateType: nomenclatures.get('aircraftTypeCertificateTypes', 'TCT1'),
        certNumber: 'EASA.IM.A.277',
        certDate: null,
        certRelease: null,
        contry: null
      }
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-registrations.sample'] = {}) : module);
