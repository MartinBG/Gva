/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове адреси
  module.exports = [
    {
      nomValueId: 5582, code: 'TO', name: 'Адрес за базово ослужване на ВС', nameAlt: 'Адрес за базово ослужване на ВС', parentValueId: null, alias: null,
      textContent: { "type": "F" }
    },
    {
      nomValueId: 5583, code: 'PER', name: 'Постоянен адрес', nameAlt: 'Постоянен адрес', parentValueId: null, alias: 'Permanent',
      textContent: { "type": "P" }
    },
    {
      nomValueId: 5584, code: 'TMP', name: 'Настоящ адрес', nameAlt: 'Настоящ адрес', parentValueId: null, alias: null,
      textContent: { "type": "P" }
    },
    {
      nomValueId: 5585, code: 'COR', name: 'Адрес за кореспонденция', nameAlt: 'Адрес за кореспонденция', parentValueId: null, alias: 'Correspondence',
      textContent: { "type": "P" }
    },
    {
      nomValueId: 5586, code: 'O', name: 'Седалище', nameAlt: 'Седалище', parentValueId: null, alias: null,
      textContent: { "type": "F" }
    },
    {
      nomValueId: 5587, code: 'TOP', name: 'Данни за ръководител', nameAlt: 'Данни за ръководител', parentValueId: null, alias: null,
      textContent: { "type": "F" }
    },
    {
      nomValueId: 5588, code: 'BOS', name: 'Данни за ръководител TO', nameAlt: 'Данни за ръководител TO', parentValueId: null, alias: null,
      textContent: { "type": "F" }
    }
  ];
})(typeof module === 'undefined' ? (this['addressType'] = {}) : module);
