/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Address1: {
      addressType: nomenclatures.get('addressTypes', 'Permanent'),
      settlement: nomenclatures.get('cities', 'Plovdiv'),
      address: 'бул.Цариградско шосе 28 ет.9',
      addressAlt: 'bul.Tsarigradski shose 28 et.9',
      phone: '',
      valid: nomenclatures.get('boolean', 'false'),
      postalCode: ''
    },
    person1Address2: {
      addressType: nomenclatures.get('addressTypes', 'Correspondence'),
      settlement: nomenclatures.get('cities', 'Plovdiv'),
      address: 'жг.Толстой бл.39 ап.40',
      addressAlt: 'jk.Tolstoy bl.39 ap.40',
      phone: '',
      valid: nomenclatures.get('boolean', 'true'),
      postalCode: ''
    }
  };
})(typeof module === 'undefined' ? (this['person-address.sample'] = {}) : module);
