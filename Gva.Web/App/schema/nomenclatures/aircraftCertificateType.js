/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 8007, code: 'CT1', name: 'Удостоверение за регистрация (стандартно)', nameAlt: null, parentValueId: null, alias: 'CT1'
    },
    {
      nomValueId: 8008, code: 'CT2', name: 'Ограничено удостоверение за регистрация', nameAlt: null, parentValueId: null, alias: 'CT2'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftCertificateType'] = {}) : module);
