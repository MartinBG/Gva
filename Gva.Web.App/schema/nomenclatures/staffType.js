/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове персонал
  module.exports = [
    { nomValueId: 0, code: 'D', name: 'Общ документ', nameAlt: 'Общ документ', alias: 'D' },
    {
      nomValueId: 5589, code: 'M', name: 'Наземен авиационен персонал за TO на СУВД', nameAlt: 'Наземен авиационен персонал за TO на СУВД', alias: 'M',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 5590, code: 'G', name: 'Наземен авиационен персонал за TO на ВС', nameAlt: 'Наземен авиационен персонал за TO', alias: 'G',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 5591, code: 'F', name: 'Членове на екипажа', nameAlt: 'Членове на екипажа', alias: 'F',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 5592, code: 'T', name: 'Наземен авиационен персонал за ОВД', nameAlt: 'Наземен авиационен персонал за ОВД', alias: 'T',
      textContent: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['staffType'] = {}) : module);
