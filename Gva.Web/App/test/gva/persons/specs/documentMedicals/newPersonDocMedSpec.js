/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person medical document new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentMedicals/docMedPO'),
        SearchPage = require('../../pageObjects/documentMedicals/searchDocMedPO'),
        newDocMedPage,
        searchDocMedPage;

    beforeEach(function () {
      ptor.get('#/persons/1/medicals/new');
      newDocMedPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocMedPage.breadcrumb.get())
        .toEqual('Новo медицинско');
    });

    it('should create new medical document correctly', function () {
      newDocMedPage.documentNumberPrefix.set('1');
      newDocMedPage.documentNumber.set('2324a');
      newDocMedPage.documentNumberSuffix.set('23');
      newDocMedPage.medClassType.set('Class-3');
      newDocMedPage.documentDateValidFrom.set('20.10.2014');
      newDocMedPage.documentDateValidTo.set('01.01.2020');
      newDocMedPage.documentPublisher.set('FAA');
      newDocMedPage.limitationsTypes.set('OCL');
      newDocMedPage.notes.set('notes');

      newDocMedPage.save();
      searchDocMedPage = new SearchPage(ptor);
      expect(searchDocMedPage.breadcrumb.get()).toEqual('Медицински');

      expect(searchDocMedPage.datatable.getColumns(
          'part_testimonial',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_medClassType_name',
          'part_limitations',
          'part_documentPublisher_name'
          )).toEqual([
        ['MED BG-1-11232-99994', '04.04.2010', '04.08.2010', 'Class-1',
          'OSL, OML', 'КАМО'],
        ['MED BG2-3244-11232-9934', '04.04.2005', '06.09.2015', 'Class-2',
          'OSL, OML, VDL', 'CAA France'],
        ['1-2324a-11232-23', '20.10.2014', '01.01.2020', 'Class-3', 'OCL', 'FAA']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newDocMedPage.cancel();
      searchDocMedPage = new SearchPage(ptor);
      expect(searchDocMedPage.breadcrumb.get()).toEqual('Медицински');
    });
  });

} (protractor, describe, beforeEach, it, expect));