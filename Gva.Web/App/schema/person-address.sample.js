(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    personAddress1: {
      addressTypeId: nomenclatures.getId('addressTypes', 'Permanent'), 
      settlementId: nomenclatures.getId('cities', 'Plovdiv'),
      address: 'бул.Цариградско шосе 28 ет.9',
      addressAlt: 'bul.Tsarigradski shose 28 et.9',
      phone: '',
      valid: false,
      postalCode: ''
    },
    personAddress2: {
      addressTypeId: nomenclatures.getId('addressTypes', 'Correspondence'), 
      address: 'жг.Толстой бл.39 ап.40',
      addressAlt: 'jk.Tolstoy bl.39 ap.40',
      phone: '',
      valid: true,
      postalCode: '',
      settlementId: nomenclatures.getId('cities', 'Plovdiv')
    }
  };
})(typeof module === 'undefined' ? (this['person-address.sample'] = {}) : module);