/*global module, require*/
(function (module) {
    'use strict';

    var nomenclatures = require('./nomenclatures.sample');

    module.exports = {
       application1: {
		documentNumber: '1122',
		documentDate: '2014-05-02T00:00',
		notes: 'Бележки',
		bookPageNumber: '2',
		pageCount: '3',
		applicationType: nomenclatures.get('applicationTypes', '7961'),
		applicationPaymentType: nomenclatures.get('applicationPaymentTypes', 'чл.117а(1)'),
		currency: nomenclatures.get('currencies', 'EUR'),
		taxAmount: 4
       },
       application2: {
       	documentNumber: '1122a',
       	documentDate: '2014-06-02T00:00',
       	notes: 'Бележки',
       	bookPageNumber: 1,
       	pageCount: 2,
       	applicationType: nomenclatures.get('applicationTypes', '7958'),
       	applicationPaymentType: nomenclatures.get('applicationPaymentTypes', 'чл. 117в'),
       	currency: nomenclatures.get('currencies', 'BGN'),
       	taxAmount: 4
       }
    };
})(typeof module === 'undefined' ? (this['person-document-application.sample'] = {}) : module);
