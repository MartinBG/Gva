/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Edit doccument view page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/docViewPO'),
       SelectUnitPage = require('../pageObjects/selectUnitPO'),
       SelectCorrPage = require('../pageObjects/selectCorrPO'),
       docViewPage,
       selectUnitPage,
       selectCorrPage;

    beforeEach(function () {
      ptor.get('#/docs/1/address');
      docViewPage = new Page(ptor);

      docViewPage.edit();
    });

    it('should update breadcrumb text', function () {
      expect(docViewPage.breadcrumb.getText())
        .toEqual('Адресати');
    });

    it('should select unit and correspondent', function () {
      docViewPage.selectUnitFrom();

      selectUnitPage = new SelectUnitPage(ptor);

      expect(selectUnitPage.breadcrumb.getText())
        .toEqual('Избор служител');

      selectUnitPage.firstSelectBtn.click();

      docViewPage = new Page(ptor);
      expect(docViewPage.breadcrumb.getText())
        .toEqual('Адресати');

      docViewPage.selectCorr();

      selectCorrPage = new SelectCorrPage(ptor);

      expect(selectCorrPage.breadcrumb.getText())
        .toEqual('Избор кореспондент');

      selectCorrPage.firstSelectBtn.click();

      docViewPage = new Page(ptor);
      expect(docViewPage.breadcrumb.getText())
        .toEqual('Адресати');
    });

  });

} (protractor, describe, beforeEach, it, expect));
