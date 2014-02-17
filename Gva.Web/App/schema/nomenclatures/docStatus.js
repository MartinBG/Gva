/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomTypeValueId: 1, code: '', name: 'Чернова', nameAlt: '', alias: 'Draft' },
    { nomTypeValueId: 2, code: '', name: 'Изготвен', nameAlt: '', alias: 'Prepared' },
    { nomTypeValueId: 3, code: '', name: 'Обработен', nameAlt: '', alias: 'Processed' },
    { nomTypeValueId: 4, code: '', name: 'Приключен', nameAlt: '', alias: 'Finished' },
    { nomTypeValueId: 5, code: '', name: 'Отхвърлен', nameAlt: '', alias: 'Canceled' }
  ];
})(typeof module === 'undefined' ? (this['docStatus'] = {}) : module);
