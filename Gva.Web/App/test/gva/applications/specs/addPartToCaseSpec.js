/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Application case page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/viewApplicationCasePO'),
       NewFileApplicationCasePage = require('../pageObjects/newFileApplicationCasePO'),
       AddPartApplicationCasePage = require('../pageObjects/addPartApplicationCasePO'),
       viewApplicationPage,
       newFileApplicationCasePage,
       addPartApplicationCasePage;

    beforeEach(function () {
      ptor.get('#/applications/1/case');
      viewApplicationPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(viewApplicationPage.breadcrumb.getText())
        .toEqual('Преписка');
    });

    it('should add new part and link existing part', function () {
      viewApplicationPage.newPart(ptor);

      newFileApplicationCasePage = new NewFileApplicationCasePage(ptor);
      expect(newFileApplicationCasePage.breadcrumb.getText())
        .toEqual('Нов документ');

      newFileApplicationCasePage.docPartType.set('Документ за самоличност');

      newFileApplicationCasePage.addPart();

      addPartApplicationCasePage = new AddPartApplicationCasePage(ptor);
      expect(addPartApplicationCasePage.breadcrumb.getText())
        .toEqual('Добавяне');

      addPartApplicationCasePage.personDocIdType.set('Лична карта');
      addPartApplicationCasePage.documentNumber.set('123 карта');
      addPartApplicationCasePage.documentDateValidFrom.set('01.01.2014');
      addPartApplicationCasePage.documentDateValidTo.set('01.01.2015');
      addPartApplicationCasePage.documentPublisher.set('МВР-София');
      addPartApplicationCasePage.bookPageNumber.set('1');

      addPartApplicationCasePage.save();

      viewApplicationPage = new Page(ptor);
      expect(viewApplicationPage.breadcrumb.getText())
        .toEqual('Преписка');
    });
  });

} (protractor, describe, beforeEach, it, expect));
