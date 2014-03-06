/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Person document other search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentOthers/searchDocOtherPO'),
        personDocOtherPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentOthers');
      personDocOtherPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocOtherPage.breadcrumb.getText()).toEqual('Други документи');
    });

    it('should display data correctly', function () {
      expect(personDocOtherPage.datatable.getColumns(
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_documentPublisher',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
        ['1221', '06.03.2010', '06.03.2013',
          'УЦ: Български въздухоплавателен център', '62', '1']
      ]);
    });

    it('should delete an other document', function () {
      personDocOtherPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[1].click();
        personDocOtherPage = new Page(ptor);
        expect(personDocOtherPage.datatable.getColumns(
        'part_documentNumber',
        'part_documentDateValidFrom',
        'part_documentDateValidTo',
        'part_documentPublisher',
        'part_bookPageNumber',
        'part_pageCount'
          )).toEqual([
          []
        ]);
      });
    });

    it('should go to edit page', function () {
        personDocOtherPage.datatable.getRowButtons(1).then(function (buttons) {
          buttons[0].click();
          expect(ptor.getCurrentUrl())
            .toEqual('http://localhost:52560/#/persons/1/documentOthers/13');
        });
      });
  });

}(protractor, describe, beforeEach, it, expect));