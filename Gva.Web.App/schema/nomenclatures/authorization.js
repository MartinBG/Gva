/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Разрешения към квалификация
  module.exports = [
    {
      nomValueId: 7176, code: 'FI(A)', name: 'Летателен инструктор на самолет', nameAlt: 'Летателен инструктор на самолет', parentValueId: 7049, alias: 'FI(A)',
      textContent: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7186, code: 'CAT II', name: 'CAT II (cop)', nameAlt: 'CAT II (cop)', parentValueId: 7048, alias: 'CAT II',
      textContent: {
        codeCA: 'CAT II',
        dateValidFrom: '2009-07-13T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7187, code: 'CAT IIIA', name: 'CAT III A (cop)', nameAlt: 'CAT IIIA (cop)', parentValueId: 7048, alias: 'CAT IIIA',
      textContent: {
        codeCA: 'CAT IIIA',
        dateValidFrom: '2009-07-13T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7188, code: 'CAT IIIB', name: 'CAT IIIB (cop)', nameAlt: 'CAT IIIB (cop)', parentValueId: 7048, alias: 'CAT IIIB',
      textContent: {
        codeCA: 'CAT IIIB',
        dateValidFrom: '2009-07-13T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7189, code: 'LV-TO', name: 'LV-TO (cop)', nameAlt: 'LV-TO (cop)', parentValueId: 7048, alias: 'LV-TO',
      textContent: {
        codeCA: 'LV-TO',
        dateValidFrom: '2009-07-13T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7134, code: 'GMS', name: 'КВД по маневрената площ на летището чрез средства за обзор', nameAlt: 'Ground Movement Surveillance ', parentValueId: 7046, alias: 'GMS',
      textContent: {
        codeCA: 'GMS',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7135, code: 'RAD', name: 'КВД чрез радар', nameAlt: 'Radar', parentValueId: 7046, alias: 'RAD',
      textContent: {
        codeCA: 'RAD',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7136, code: 'ADS', name: 'КВД чрез автоматичен зависим обзор', nameAlt: 'Automatic Dependent Surveillance', parentValueId: 7046, alias: 'ADS',
      textContent: {
        codeCA: 'ADS',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7137, code: 'PAR', name: 'КВД чрез прецизен радар за подход', nameAlt: 'Precision Approach Radar', parentValueId: 7046, alias: 'PAR',
      textContent: {
        codeCA: 'PAR',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7138, code: 'SRA', name: 'КВД чрез обзорен радар за подход', nameAlt: 'Surveillance Radar Approach ', parentValueId: 7046, alias: 'SRA',
      textContent: {
        codeCA: 'SRA',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7150, code: 'FE(A)', name: 'FE(A)', nameAlt: 'FE(A)', parentValueId: 7045, alias: 'FE(A)',
      textContent: {
        codeCA: 'FE(A)',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7158, code: 'TRE(H)', name: 'TRE(H)', nameAlt: 'TRE(H)', parentValueId: 7045, alias: 'TRE(H)',
      textContent: {
        codeCA: 'TRE(H)',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTó: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7169, code: 'TRE(A)', name: 'TRE(A)', parentValueId: 7045, alias: 'TRE(A)',
      textContent: {
        codeCA: 'TRE(A)',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7190, code: 'GAREX 220 SIMULATOR', name: 'GAREX 220 SIMULATOR', nameAlt: 'GAREX 220 SIMULATOR', parentValueId: 7050, alias: 'GAREX 220 SIMULATOR',
      textContent: {
        codeCA: 'GAREX 220 SIMULATOR',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7194, code: 'DVOR 432', name: 'DVOR 432', nameAlt: 'DVOR 432', parentValueId: 7050, alias: 'DVOR 432',
      textContent: {
        codeCA: 'DVOR 432',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['authorization'] = {}) : module);
