/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Класове ВС за екипажи
  module.exports = [
    {
      nomTypeValueId: 7000, code: 'VLA', name: 'Много леки самолети', nameAlt: 'Много леки самолети', nomTypeParentValueId: 6986, alias: 'VLA',
      content: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7001, code: 'ULA', name: 'Свръхлеки самолети', nameAlt: 'Свръхлеки самолети', nomTypeParentValueId: 6986, alias: 'ULA',
      content: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7009, code: 'C', name: 'Радиолокационен обзор', nameAlt: 'Surveillance', nomTypeParentValueId: 6987, alias: 'C',
      content: {
        codeCA: 'C',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7010, code: 'D', name: 'Обработка на данни', nameAlt: 'Data processing', nomTypeParentValueId: 6987, alias: 'D',
      content: {
        codeCA: 'D',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7011, code: 'E', name: 'Аеронавигационно метеорологично оборудване', nameAlt: 'Met', nomTypeParentValueId: 6987, alias: 'E',
      content: {
        codeCA: 'E',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7012, code: 'F', name: 'Светотехнически средства', nameAlt: 'Agl', nomTypeParentValueId: 6987, alias: 'F',
      content: {
        codeCA: 'F',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 7007, code: 'A', name: 'Комуникация', nameAlt: 'Communications', nomTypeParentValueId: 6987, alias: 'A',
      content: {
        codeCA: 'A',
        dateValidFrom: '2011-03-18T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6991, code: 'APP', name: 'Процедурно ОВД в летищния контролиран район', nameAlt: 'Aerodrome Control Procedural', nomTypeParentValueId: 6985, alias: 'APP',
      content: {
        codeCA: 'APP',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6992, code: 'APS', name: 'ОВД в летищния контролиран районн чрез средства за обзор', nameAlt: 'Approach Control Surveillance', nomTypeParentValueId: 6985, alias: 'APS',
      content: {
        codeCA: 'APS',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 102, code: 'ACP', name: 'Процедурно ОВД в контролирания район', nameAlt: 'Area Control Procedural', nomTypeParentValueId: 6985, alias: 'ACP',
      content: {
        codeCA: 'ACP',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['ratingClass'] = {}) : module);
