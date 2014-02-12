/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове адреси
  module.exports = [
    {
      nomTypeValueId: 5582, code: 'TO', name: 'Адрес за базово ослужване на ВС', nameAlt: 'Адрес за базово ослужване на ВС', nomTypeParentValueId: null, alias: null,
      content: { "type": "F" }
    },
    {
      nomTypeValueId: 5583, code: 'PER', name: 'Постоянен адрес', nameAlt: 'Постоянен адрес', nomTypeParentValueId: null, alias: 'Permanent',
      content: { "type": "P" }
    },
    {
      nomTypeValueId: 5584, code: 'TMP', name: 'Настоящ адрес', nameAlt: 'Настоящ адрес', nomTypeParentValueId: null, alias: null,
      content: { "type": "P" }
    },
    {
      nomTypeValueId: 5585, code: 'COR', name: 'Адрес за кореспонденция', nameAlt: 'Адрес за кореспонденция', nomTypeParentValueId: null, alias: 'Correspondence',
      content: { "type": "P" }
    },
    {
      nomTypeValueId: 5586, code: 'O', name: 'Седалище', nameAlt: 'Седалище', nomTypeParentValueId: null, alias: null,
      content: { "type": "F" }
    },
    {
      nomTypeValueId: 5587, code: 'TOP', name: 'Данни за ръководител', nameAlt: 'Данни за ръководител', nomTypeParentValueId: null, alias: null,
      content: { "type": "F" }
    },
    {
      nomTypeValueId: 5588, code: 'BOS', name: 'Данни за ръководител TO', nameAlt: 'Данни за ръководител TO', nomTypeParentValueId: null, alias: null,
      content: { "type": "F" }
    }
  ];
})(typeof module === 'undefined' ? (this['addressType'] = {}) : module);
