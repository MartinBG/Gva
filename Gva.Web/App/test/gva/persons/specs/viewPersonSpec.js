/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('View person page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/viewPersonPO'),
        viewPersonPage;

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
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/personData');
    });

    it('should navigate to different sections', function () {
      var licencesTab = ptor.findElement(protractor.By.linkText('Лицензи')),
          personDataTab = ptor.findElement(protractor.By.linkText('Лични данни'));

      licencesTab.click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/licences');

      personDataTab.click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses');

      ptor.findElement(protractor.By.linkText('Състояния')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));