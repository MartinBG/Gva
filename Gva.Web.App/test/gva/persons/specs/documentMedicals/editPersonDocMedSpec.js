﻿/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document medical edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentMedicals/docMedPO'),
        SearchPage = require('../../pageObjects/documentMedicals/searchDocMedPO'),
        editDocMedPage,
        searchDocMedPage;

    beforeEach(function() {
      ptor.get('#/persons/1/medicals/18');
      editDocMedPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editDocMedPage.breadcrumb.get()).toEqual('Редакция на медицинско');
    });

    it('should display correct filled out data', function () {
      expect(editDocMedPage.documentNumberPrefix.get()).toEqual('MED BG');
      expect(editDocMedPage.documentNumber.get()).toEqual('1');
      expect(editDocMedPage.documentNumberSuffix.get()).toEqual('99994');
      expect(editDocMedPage.medClass.get()).toEqual('Class-1');
      expect(editDocMedPage.documentDateValidFrom.get()).toEqual('04.04.2010');
      expect(editDocMedPage.documentDateValidTo.get()).toEqual('04.08.2010');
      expect(editDocMedPage.documentPublisher.get()).toEqual('КАМО');
      expect(editDocMedPage.notes.get()).toEqual('Test notes');
      expect(editDocMedPage.limitations.get()).toEqual(['OSL', 'OML']);
    });
    
    it('should change medical data correctly', function () {
      editDocMedPage.documentNumberPrefix.set('1');
      editDocMedPage.documentNumber.set('2324a');
      editDocMedPage.documentNumberSuffix.set('23');
      editDocMedPage.medClass.set('Class-3');
      editDocMedPage.documentDateValidFrom.set('20.10.2014');
      editDocMedPage.documentDateValidTo.set('01.01.2020');
      editDocMedPage.documentPublisher.set('FAA');
      editDocMedPage.notes.set('notes');

      editDocMedPage.save();
      searchDocMedPage = new SearchPage(ptor);
      expect(searchDocMedPage.breadcrumb.get()).toEqual('Медицински');

      expect(searchDocMedPage.datatable.getColumns(
          'part_testimonial',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_medClass_name',
          'part_limitations',
          'part_documentPublisher_name'
          )).toEqual([
        ['1-2324a-11232-23', '20.10.2014', '01.01.2020', 'Class-3', 'OSL, OML', 'FAA'],
        ['MED BG2-3244-11232-9934', '04.04.2005', '06.09.2015', 'Class-2',
          'OSL, OML, VDL', 'CAA France']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));