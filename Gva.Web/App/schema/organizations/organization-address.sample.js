/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Address1: {
      addressType: nomenclatures.get('addressTypes', 'Permanent'),
      valid: nomenclatures.get('boolean', 'true'),
      settlement: nomenclatures.get('cities', 'Sofia'),
      address: 'Студ. град',
      addressAlt: ' Студ. град',
      phone: '0888609823, 23-32-12',
      fax: 'Факс',
      postalCode: '1000',
      contactPerson: nomenclatures.get('persons', 'P1'),
      email: 'example@gmail.com'
    },
    organization1Address2: {
      addressType: nomenclatures.get('addressTypes', 'Correspondence'),
      valid: nomenclatures.get('boolean', 'false'),
      settlement: nomenclatures.get('cities', 'Sofia'),
      address: 'кв. Мусагеница',
      addressAlt: 'кв. Мусагеница',
      phone: '0888604323, 43-00-33',
      fax: 'Факс 2',
      postalCode: '1030',
      contactPerson: nomenclatures.get('persons', 'P2'),
      email: 'example2@gmail.com'
    }
  };
})(typeof module === 'undefined' ? (this['organization-address.sample'] = {}) : module);