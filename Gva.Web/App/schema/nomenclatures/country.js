/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    {
      nomTypeValueId: 5, code: 'AR', name: 'Аржентина', nameAlt: 'Argentina', nomTypeParentValueId: null, alias: 'AR',
      content: {'NationalityCodeCA':'AR','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'AR'}
    },
    {
      nomTypeValueId: 6, code: 'AZ', name: 'Азербайджан', nameAlt: 'Azerbaijan', nomTypeParentValueId: null, alias: 'AZ',
      content: {'NationalityCodeCA':'AZ','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'AZ'}
    },
    {
      nomTypeValueId: 8, code: 'MW', name: 'Малави', nameAlt: 'Malawi', nomTypeParentValueId: null, alias: 'MW',
      content: {'NationalityCodeCA':'MW','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'MW'}
    },
    {
    	nomTypeValueId: 10, code: 'HR', name: 'Republic of Groatia', nameAlt: 'Republic of Groatia', nomTypeParentValueId: null, alias: 'HR',
    	content: {'NationalityCodeCA':'HR','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'HR'}
    },
    {
      nomTypeValueId: 14, code: 'KWI', name: 'Кувейт', nameAlt: 'Kuwait', nomTypeParentValueId: null, alias: 'KWI',
      content: {'NationalityCodeCA':'KWI','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'KWI'}
    },
    {
      nomTypeValueId: 15, code: 'ISL', name: 'Исландия', nameAlt: 'Iceland', nomTypeParentValueId: null, alias: 'ISL',
      content: {'NationalityCodeCA':'ISL','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'ISL'}
    },
    {
      nomTypeValueId: 17, code: 'ZA', name: 'Република Южна Африка', nameAlt: 'Republic of South Africa', nomTypeParentValueId: null, alias: 'ZA',
      content: {'NationalityCodeCA':'ZA','Heading':null,'HeadingAlt':null,'LicenceCodeCA':'ZA'}
    },
    {
      nomTypeValueId: 58, code: 'BG', name: 'Република България', nameAlt: 'Republic of Bulgaria', nomTypeParentValueId: null, alias: 'BG',
      content: { 'NationalityCodeCA': 'BGR', 'Heading': 'РЕПУБЛИКА БЪЛГАРИЯ', 'HeadingAlt': 'REPUBLIC OF BULGARIA', 'LicenceCodeCA': 'BGR.' }
    },
    {
      nomTypeValueId: 5068, code: '00 00', name: 'Йоханесбург', nameAlt: 'Johannesburg', nomTypeParentValueId: 17, alias: null,
      content: { 'type': 'T', 'notes': null, 'postCode': null, 'oblCode': null, 'obstCode': null }
    }
  ]
})(typeof module === 'undefined' ? (this['country'] = {}) : module);
