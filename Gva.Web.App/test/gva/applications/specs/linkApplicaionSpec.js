/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Link application page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/linkApplicationPO'),
       SearchPage = require('../pageObjects/searchApplicationPO'),
       ViewApplicationCasePage = require('../pageObjects/viewApplicationCasePO'),
       SelectDocumentPage = require('../pageObjects/selectDocumentPO'),
       linkApplicationPage,
       searchApplicationPage,
       viewApplicationCasePage,
       selectDocumentPage;

    beforeEach(function () {
      ptor.get('#/applications/link');
      linkApplicationPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(linkApplicationPage.breadcrumb.getText())
        .toEqual('Свържи заявление');
    });

    it('should go to app search on cancel click', function () {
      linkApplicationPage.cancel();

      searchApplicationPage = new SearchPage(ptor);

      expect(searchApplicationPage.breadcrumb.getText())
        .toEqual('Заявления');
    });

    it('should link application correctly', function () {
      linkApplicationPage.selectDoc();

      selectDocumentPage = new SelectDocumentPage(ptor);
      expect(selectDocumentPage.breadcrumb.getText())
        .toEqual('Избор на документ');

      selectDocumentPage.selectFirstDoc();

      linkApplicationPage = new Page(ptor);
      expect(linkApplicationPage.breadcrumb.getText())
        .toEqual('Свържи заявление');

      linkApplicationPage.person.set('Иван Иванов');

      linkApplicationPage.link();

      viewApplicationCasePage = new ViewApplicationCasePage(ptor);

      expect(viewApplicationCasePage.breadcrumb.getText())
        .toEqual('Преписка');
    });
  });

} (protractor, describe, beforeEach, it, expect));
