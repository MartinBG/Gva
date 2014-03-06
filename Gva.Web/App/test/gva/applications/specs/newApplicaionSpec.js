/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('New application page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/newApplicationPO'),
       SearchPage = require('../pageObjects/searchApplicationPO'),
       ViewApplicationCasePage = require('../pageObjects/viewApplicationCasePO'),
       newApplicationPage,
       searchApplicationPage,
       viewApplicationCasePage;

    beforeEach(function () {
      ptor.get('#/applications/new');
      newApplicationPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newApplicationPage.breadcrumb.getText())
        .toEqual('Ново заявление');
    });

    it('should go to app search on cancel click', function () {
      newApplicationPage.cancel();

      searchApplicationPage = new SearchPage(ptor);

      expect(searchApplicationPage.breadcrumb.getText())
        .toEqual('Заявления');
    });

    it('should create new application correctly', function () {
      newApplicationPage.person.set('Иван Иванов');
      newApplicationPage.docTypeGroup.set('Общи');
      newApplicationPage.docType.set('Резолюция');
      newApplicationPage.docSubject.set('Относно');

      newApplicationPage.register();

      viewApplicationCasePage = new ViewApplicationCasePage(ptor);

      expect(viewApplicationCasePage.breadcrumb.getText())
        .toEqual('Преписка');
    });
  });

} (protractor, describe, beforeEach, it, expect));
