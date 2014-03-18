/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person medical document search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentMedicals/searchDocMedPO'),
        personDocMedPage;

    beforeEach(function () {
      ptor.get('#/persons/1/medicals');
      personDocMedPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocMedPage.breadcrumb.getText()).toEqual('Медицински');
    });

    it('should display data correctly', function () {
      expect(personDocMedPage.datatable.getColumns(
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
          'OSL, OML, VDL', 'CAA France']
      ]);
    });
   
    it('should delete a medical document', function () {
      personDocMedPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[1].click();
        personDocMedPage = new Page(ptor);
        expect(personDocMedPage.datatable.getColumns(
          'part_testimonial',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_medClassType_name',
          'part_limitations',
          'part_documentPublisher_name'
          )).toEqual([
          ['MED BG-1-11232-99994', '04.04.2010', '04.08.2010',
            'Class-1', 'OSL, OML', 'КАМО']
        ]);
      });
    });

    it('should go to edit page', function () {
      personDocMedPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/medicals/18');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));