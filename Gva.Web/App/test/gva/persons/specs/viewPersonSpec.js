/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('View person page', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/persons/1');
    });

    it('should display person', function () {
      var personName = ptor.findElement(protractor.By.name('names')),
          personCompany = ptor.findElement(protractor.By.name('organization')),
          personEmplCategory = ptor.findElement(protractor.By.name('employment')),
          personUin = ptor.findElement(protractor.By.name('uin')),
          personLin = ptor.findElement(protractor.By.name('lin')),
          personAge = ptor.findElement(protractor.By.name('age'));

      expect(personName.isEnabled()).toBe(false);
      expect(personName.getAttribute('value')).toEqual('Иван Иванов Иванов');

      expect(personCompany.isEnabled()).toBe(false);
      expect(personCompany.getAttribute('value')).toEqual('AAK Progres');

      expect(personEmplCategory.isEnabled()).toBe(false);
      expect(personEmplCategory.getAttribute('value')).toEqual('Ученик Ръководител Полети');

      expect(personUin.isEnabled()).toBe(false);
      expect(personUin.getAttribute('value')).toEqual('7005159385');

      expect(personLin.isEnabled()).toBe(false);
      expect(personLin.getAttribute('value')).toEqual('11232');

      expect(personAge.isEnabled()).toBe(false);
      expect(personAge.getAttribute('value')).toEqual('43');
    });

    it('should navigate to edit person page', function () {
      ptor.findElement(protractor.By.name('editBtn')).click();
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
}(protractor, describe, beforeEach, it, expect));