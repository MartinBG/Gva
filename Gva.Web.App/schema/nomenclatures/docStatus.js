/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomValueId: 1, code: '', name: 'Чернова', nameAlt: '', alias: 'Draft' },
    { nomValueId: 2, code: '', name: 'Изготвен', nameAlt: '', alias: 'Prepared' },
    { nomValueId: 3, code: '', name: 'Обработен', nameAlt: '', alias: 'Processed' },
    { nomValueId: 4, code: '', name: 'Приключен', nameAlt: '', alias: 'Finished' },
    { nomValueId: 5, code: '', name: 'Отхвърлен', nameAlt: '', alias: 'Canceled' }
  ];
})(typeof module === 'undefined' ? (this['docStatus'] = {}) : module);
