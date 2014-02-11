/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Типове документи за самоличност на Физичеко лице
  module.exports = [
    { nomTypeValueId: 6442, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: 'Id' },
    { nomTypeValueId: 6443, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт',alias: null },
    { nomTypeValueId: 6444, code: '5', name: 'Паспорт', nameAlt: 'Паспорт', alias: 'passport' }
  ];
})(typeof module === 'undefined' ? (this['personIdDocumentType'] = {}) : module);
