/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове персонал
  module.exports = [
    { nomTypeValueId: 0, code: 'D', name: 'Общ документ', nameAlt: 'Общ документ', alias: 'D' },
    {
      nomTypeValueId: 5589, code: 'M', name: 'Наземен авиационен персонал за TO на СУВД', nameAlt: 'Наземен авиационен персонал за TO на СУВД', alias: 'M',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 5590, code: 'G', name: 'Наземен авиационен персонал за TO на ВС', nameAlt: 'Наземен авиационен персонал за TO', alias: 'G',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 5591, code: 'F', name: 'Членове на екипажа', nameAlt: 'Членове на екипажа', alias: 'F',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 5592, code: 'T', name: 'Наземен авиационен персонал за ОВД', nameAlt: 'Наземен авиационен персонал за ОВД', alias: 'T',
      content: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['staffType'] = {}) : module);
