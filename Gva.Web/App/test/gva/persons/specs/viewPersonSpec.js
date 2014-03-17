/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('View person page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/viewPersonPO'),
        PersonDataPage = require('../pageObjects/personDataPO'),
        viewPersonPage,
        personDataPage;

    beforeEach(function () {
      ptor.get('#/persons/1');
      viewPersonPage = new Page(ptor);
    });

    it('should display person', function () {
      expect(viewPersonPage.name.isEnabled()).toBe(false);
      expect(viewPersonPage.name.getAttribute('value')).toEqual('Иван Иванов Иванов');

      expect(viewPersonPage.company.isEnabled()).toBe(false);
      expect(viewPersonPage.company.getAttribute('value')).toEqual('AAK Progres');

      expect(viewPersonPage.emplCategory.isEnabled()).toBe(false);
      expect(viewPersonPage.emplCategory.getAttribute('value'))
        .toEqual('Ученик Ръководител Полети');

      expect(viewPersonPage.uin.isEnabled()).toBe(false);
      expect(viewPersonPage.uin.getAttribute('value')).toEqual('7005159385');

      expect(viewPersonPage.lin.isEnabled()).toBe(false);
      expect(viewPersonPage.lin.getAttribute('value')).toEqual('11232');

      expect(viewPersonPage.age.isEnabled()).toBe(false);
      expect(viewPersonPage.age.getAttribute('value')).toEqual('43');
    });

    it('should navigate to edit person page', function () {
      viewPersonPage.edit();
      personDataPage = new PersonDataPage(ptor);
      expect(personDataPage.breadcrumb.get()).toEqual('Редакция');
    });

    it('should navigate to different sections', function () {
      viewPersonPage.tabs.clickTab('Лицензи');
      viewPersonPage = new Page(ptor);
      expect(viewPersonPage.breadcrumb.get()).toEqual('Лицензи');
      
      viewPersonPage.tabs.clickTab('Лични данни');
      viewPersonPage = new Page(ptor);
      expect(viewPersonPage.breadcrumb.get()).toEqual('Адреси');

      viewPersonPage.tabs.clickTab('Състояния');
      viewPersonPage = new Page(ptor);
      expect(viewPersonPage.breadcrumb.get()).toEqual('Състояния');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));