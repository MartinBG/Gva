/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Person document other search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentOthers/searchDocOtherPO'),
        EditPage = require('../../pageObjects/documentOthers/docOtherPO'),
        personDocOtherPage,
        editDocOtherPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentOthers');
      personDocOtherPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocOtherPage.breadcrumb.get()).toEqual('Други документи');
    });

    it('should display data correctly', function () {
      expect(personDocOtherPage.datatable.getColumns(
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_documentPublisher'
          )).toEqual([
        ['1221', '06.03.2010', '06.03.2013', 'УЦ: Български въздухоплавателен център']
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
        'part_documentPublisher'
          )).toEqual([
          []
        ]);
      });
    });

    it('should go to edit page', function () {
        personDocOtherPage.datatable.getRowButtons(1).then(function (buttons) {
          buttons[0].click();
          editDocOtherPage = new EditPage(ptor);
          expect(editDocOtherPage.breadcrumb.get()).toEqual('Редакция на документ');
        });
      });
  });

}(protractor, describe, beforeEach, it, expect));