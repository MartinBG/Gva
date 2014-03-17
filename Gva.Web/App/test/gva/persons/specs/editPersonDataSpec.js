/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Edit person data page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/personDataPO'),
        ViewPage = require('../pageObjects/viewPersonPO'),
        editPersonDataPage,
        viewPersonDataPage;

    beforeEach(function () {
      ptor.get('#/persons/1/personData');
      editPersonDataPage = new Page(ptor);
    });

    it('should display person\'s data', function () {
      expect(editPersonDataPage.firstName.get()).toEqual('Иван');
      expect(editPersonDataPage.middleName.get()).toEqual('Иванов');
      expect(editPersonDataPage.lastName.get()).toEqual('Иванов');
      expect(editPersonDataPage.firstNameAlt.get()).toEqual('Ivan');
      expect(editPersonDataPage.middleNameAlt.get()).toEqual('Ivanov');
      expect(editPersonDataPage.lastNameAlt.get()).toEqual('Ivanov');
      expect(editPersonDataPage.lin.get()).toEqual('11232');
      expect(editPersonDataPage.uin.get()).toEqual('7005159385');
      expect(editPersonDataPage.sex.get()).toEqual('Мъж');
      expect(editPersonDataPage.dateOfBirth.get()).toEqual('15.05.1970');
      expect(editPersonDataPage.placeOfBirth.get()).toEqual('София');
      expect(editPersonDataPage.country.get()).toEqual('Република България');
      expect(editPersonDataPage.email.get()).toEqual('ivan@mail.bg');
      expect(editPersonDataPage.fax.get()).toEqual('(02) 876 89 89');
      expect(editPersonDataPage.phone1.get()).toEqual('0888 876 431');
      expect(editPersonDataPage.phone2.get()).toEqual('0888 876 432');
      expect(editPersonDataPage.phone3.get()).toEqual('0888 876 433');
      expect(editPersonDataPage.phone4.get()).toEqual('0888 876 434');
      expect(editPersonDataPage.phone5.get()).toEqual('0888 876 435');
    });

    it('should be able to change person data', function () {
      editPersonDataPage.firstName.set('Vasil');

      editPersonDataPage.save();

      viewPersonDataPage = new ViewPage(ptor);
      expect(viewPersonDataPage.breadcrumb.get()).toEqual('Лично досие');

      expect(ptor.findElement(protractor.By.name('names')).getAttribute('value'))
        .toEqual('Vasil Иванов Иванов');
    });

    it('should be able to return to view person page', function () {
      editPersonDataPage.cancel();
      viewPersonDataPage = new ViewPage(ptor);
      expect(viewPersonDataPage.breadcrumb.get()).toEqual('Лично досие');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));