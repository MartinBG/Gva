/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Edit person data page', function () {
    var ptor = protractor.getInstance(),
        firstName,
        middleName,
        lastName,
        firstNameAlt,
        middleNameAlt,
        lastNameAlt,
        lin,
        uin,
        sex,
        dateOfBirth,
        placeOfBirth,
        country,
        email,
        fax,
        phone1,
        phone2,
        phone3,
        phone4,
        phone5;

    beforeEach(function () {
      ptor.get('#/persons/1/personData');

      firstName = ptor.findElement(protractor.By.input('model.firstName'));
      middleName = ptor.findElement(protractor.By.input('model.middleName'));
      lastName = ptor.findElement(protractor.By.input('model.lastName'));
      firstNameAlt = ptor.findElement(protractor.By.input('model.firstNameAlt'));
      middleNameAlt = ptor.findElement(protractor.By.input('model.middleNameAlt'));
      lastNameAlt = ptor.findElement(protractor.By.input('model.lastNameAlt'));
      lin = ptor.findElement(protractor.By.input('model.lin'));
      uin = ptor.findElement(protractor.By.input('model.uin'));
      sex = ptor.findElement(protractor.By.input('model.sex'));
      dateOfBirth = ptor.findElement(protractor.By.css('div.date > input'));
      placeOfBirth = ptor.findElement(protractor.By.input('model.placeOfBirth'));
      country = ptor.findElement(protractor.By.input('model.country'));
      email = ptor.findElement(protractor.By.input('model.email'));
      fax = ptor.findElement(protractor.By.input('model.fax'));
      phone1 = ptor.findElement(protractor.By.input('model.phone1'));
      phone2 = ptor.findElement(protractor.By.input('model.phone2'));
      phone3 = ptor.findElement(protractor.By.input('model.phone3'));
      phone4 = ptor.findElement(protractor.By.input('model.phone4'));
      phone5 = ptor.findElement(protractor.By.input('model.phone5'));
    });

    it('should display person\'s data', function () {
      expect(firstName.getAttribute('value')).toEqual('Иван');

      expect(middleName.getAttribute('value')).toEqual('Иванов');

      expect(lastName.getAttribute('value')).toEqual('Иванов');

      expect(firstNameAlt.getAttribute('value')).toEqual('Ivan');

      expect(middleNameAlt.getAttribute('value')).toEqual('Ivanov');

      expect(lastNameAlt.getAttribute('value')).toEqual('Ivanov');

      expect(lin.getAttribute('value')).toEqual('11232');

      expect(uin.getAttribute('value')).toEqual('6101033765');

      expect(sex.getAttribute('value')).toEqual('1');

      expect(dateOfBirth.getAttribute('value')).toEqual('04.04.1961');

      expect(placeOfBirth.getAttribute('value')).toEqual('4159');

      expect(country.getAttribute('value')).toEqual('33');

      expect(email.getAttribute('value')).toEqual('ivan@mail.bg');

      expect(fax.getAttribute('value')).toEqual('(02) 876 89 89');

      expect(phone1.getAttribute('value')).toEqual('0888 876 431');

      expect(phone2.getAttribute('value')).toEqual('0888 876 432');

      expect(phone3.getAttribute('value')).toEqual('0888 876 433');

      expect(phone4.getAttribute('value')).toEqual('0888 876 434');

      expect(phone5.getAttribute('value')).toEqual('0888 876 435');
    });

    it('should be able to change person data', function () {
      firstName.clear();
      firstName.sendKeys('Vasil');

      ptor.findElement(protractor.By.name('saveBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1');

      expect(ptor.findElement(protractor.By.name('names')).getAttribute('value'))
        .toEqual('Vasil Иванов Иванов');
    });

    it('should be able to return to view person page', function () {
      ptor.findElement(protractor.By.name('cancelBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1');
    });
  });
}(protractor, describe, beforeEach, it, expect));