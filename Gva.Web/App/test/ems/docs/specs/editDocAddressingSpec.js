/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Edit doccument addressing page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/docAddressingPO'),
       SelectUnitPage = require('../pageObjects/selectUnitPO'),
       SelectCorrPage = require('../pageObjects/selectCorrPO'),
       docAddressingPage,
       selectUnitPage,
       selectCorrPage;

    beforeEach(function () {
      ptor.get('#/docs/1/address');
      docAddressingPage = new Page(ptor);

      docAddressingPage.edit();
    });

    it('should update breadcrumb text', function () {
      expect(docAddressingPage.breadcrumb.getText())
        .toEqual('Адресати');
    });

    it('should select unit and correspondent', function () {
      docAddressingPage.selectUnitFrom();

      selectUnitPage = new SelectUnitPage(ptor);

      expect(selectUnitPage.breadcrumb.getText())
        .toEqual('Избор служител');

      selectUnitPage.firstSelectBtn.click();

      docAddressingPage = new Page(ptor);
      expect(docAddressingPage.breadcrumb.getText())
        .toEqual('Адресати');

      docAddressingPage.selectCorr();

      selectCorrPage = new SelectCorrPage(ptor);

      expect(selectCorrPage.breadcrumb.getText())
        .toEqual('Избор кореспондент');

      selectCorrPage.firstSelectBtn.click();

      docAddressingPage = new Page(ptor);
      expect(docAddressingPage.breadcrumb.getText())
        .toEqual('Адресати');
    });

  });

} (protractor, describe, beforeEach, it, expect));
