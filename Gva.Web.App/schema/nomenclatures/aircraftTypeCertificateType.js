/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 8009, code: 'TCT1', name: 'EASA типов сертификат', nameAlt: null, parentValueId: null, alias: 'TCT1'
    },
    {
      nomValueId: 8010, code: 'TCT2', name: 'Ограничен EASA типов сертификат', nameAlt: null, parentValueId: null, alias: 'TCT2'
    },
    {
      nomValueId: 8011, code: 'TCT3', name: 'Специфична сертификация', nameAlt: null, parentValueId: null, alias: 'TCT3'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftTypeCertificateType'] = {}) : module);
