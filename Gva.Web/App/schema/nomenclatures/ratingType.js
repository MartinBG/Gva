/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове ВС за екипажи
  module.exports = [
    {
      nomTypeValueId: 6908, code: 'MD80', name: 'McDonnell Douglas MD80', nameAlt: 'McDonnell Douglas MD80', alias: 'MD80',
      content: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6909, code: 'B737', name: 'Boeing 737', nameAlt: 'B737', alias: 'B737',
      content: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6910, code: 'L410', name: 'Let L-410 Turbolet', nameAlt: 'Let L-410 Turbolet', alias: 'L410',
      content: {
        codeCA: null,
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2011-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6898, code: 'IR(MEA)', name: 'Полети по прибори', nameAlt: 'Instrument rating (MEA)', alias: 'IR(MEA)',
      content: {
        codeCA: 'IR(MEA)',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6912, code: 'Тu 154', name: 'Tу 154', nameAlt: 'Tu 154', alias: 'Тu 154',
      content: {
        codeCA: 'Тu 154',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    },
    {
      nomTypeValueId: 6913, code: 'BAe 146', name: 'BAe 146', nameAlt: 'BAe 146', alias: 'BAe 146',
      content: {
        codeCA: 'BAe 146',
        dateValidFrom: '1900-01-01T00:00:00.000Z',
        dateValidTo: '2100-01-01T00:00:00.000Z'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['ratingType'] = {}) : module);
