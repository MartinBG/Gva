/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    { nomTypeValueId: 1, code: '', name: 'Български гражданин', nameAlt: 'BulgarianCitizen', alias: 'BulgarianCitizen' },
    { nomTypeValueId: 2, code: '', name: 'Чужденец', nameAlt: 'Foreigner', alias: 'Foreigner' },
    { nomTypeValueId: 3, code: '', name: 'Юридическо лице', nameAlt: 'LegalEntity', alias: 'LegalEntity' },
    { nomTypeValueId: 4, code: '', name: 'Чуждестранно юридическо лице', nameAlt: 'ForeignLegalEntity', alias: 'ForeignLegalEntity' }
  ];
})(typeof module === 'undefined' ? (this['correspondentType'] = {}) : module);
