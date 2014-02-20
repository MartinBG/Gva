/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Класове ВС за екипажи
  module.exports = [
    {
      nomValueId: 7000, code: 'VLA', name: 'Много леки самолети', nameAlt: 'Много леки самолети', parentValueId: 6986, alias: 'VLA',
      textContent: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7001, code: 'ULA', name: 'Свръхлеки самолети', nameAlt: 'Свръхлеки самолети', parentValueId: 6986, alias: 'ULA',
      textContent: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7009, code: 'C', name: 'Радиолокационен обзор', nameAlt: 'Surveillance', parentValueId: 6987, alias: 'C',
      textContent: {
        codeCA: 'C',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7010, code: 'D', name: 'Обработка на данни', nameAlt: 'Data processing', parentValueId: 6987, alias: 'D',
      textContent: {
        codeCA: 'D',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7011, code: 'E', name: 'Аеронавигационно метеорологично оборудване', nameAlt: 'Met', parentValueId: 6987, alias: 'E',
      textContent: {
        codeCA: 'E',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7012, code: 'F', name: 'Светотехнически средства', nameAlt: 'Agl', parentValueId: 6987, alias: 'F',
      textContent: {
        codeCA: 'F',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 7007, code: 'A', name: 'Комуникация', nameAlt: 'Communications', parentValueId: 6987, alias: 'A',
      textContent: {
        codeCA: 'A',
        dateValidFrom: '2011-03-18T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 6991, code: 'APP', name: 'Процедурно ОВД в летищния контролиран район', nameAlt: 'Aerodrome Control Procedural', parentValueId: 6985, alias: 'APP',
      textContent: {
        codeCA: 'APP',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 6992, code: 'APS', name: 'ОВД в летищния контролиран районн чрез средства за обзор', nameAlt: 'Approach Control Surveillance', parentValueId: 6985, alias: 'APS',
      textContent: {
        codeCA: 'APS',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomValueId: 102, code: 'ACP', name: 'Процедурно ОВД в контролирания район', nameAlt: 'Area Control Procedural', parentValueId: 6985, alias: 'ACP',
      textContent: {
        codeCA: 'ACP',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['ratingClass'] = {}) : module);
