/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomValueId: 1, code: '', name: 'Български гражданин', nameAlt: 'BulgarianCitizen', alias: 'BulgarianCitizen' },
    { nomValueId: 2, code: '', name: 'Чужденец', nameAlt: 'Foreigner', alias: 'Foreigner' },
    { nomValueId: 3, code: '', name: 'Юридическо лице', nameAlt: 'LegalEntity', alias: 'LegalEntity' },
    { nomValueId: 4, code: '', name: 'Чуждестранно юридическо лице', nameAlt: 'ForeignLegalEntity', alias: 'ForeignLegalEntity' }
  ];
})(typeof module === 'undefined' ? (this['correspondentType'] = {}) : module);
