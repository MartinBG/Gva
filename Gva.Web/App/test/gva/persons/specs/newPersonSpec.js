/*global protractor, describe, beforeEach, it, expect, require, xit*/
(function (protractor, describe, beforeEach, it, expect, require, xit) {
  'use strict';

  describe('New person page', function () {
    var ptor = protractor.getInstance(),
      Page = require('../pageObjects/newPersonPO'),
      SearchPage = require('../pageObjects/personsPO'),
      newPersonPage,
      searchPersonsPage;

    beforeEach(function () {
      ptor.get('#/persons/new');

      newPersonPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newPersonPage.breadcrumb.getText()).toEqual('Ново физическо лице');
    });

    it('should redirect to search page on cancel', function () {
      newPersonPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons');
    });

    xit('should disable save button when form is not valid', function () {
      expect(newPersonPage.isSaveBtnDisabled).toEqual(true);
    });

    it('should be able to create new person', function () {
      newPersonPage.personData.firstName.set('Георги');
      newPersonPage.personData.middleName.set('Георгиев');
      newPersonPage.personData.lastName.set('Георгиев');
      newPersonPage.personData.firstNameAlt.set('Georgi');
      newPersonPage.personData.middleNameAlt.set('Georgiev');
      newPersonPage.personData.lastNameAlt.set('Georgiev');
      newPersonPage.personData.sex.set('мъж');
      newPersonPage.personData.lin.set('99999');
      newPersonPage.personData.uin.set('9999999999');
      newPersonPage.personData.dateOfBirth.set('01.01.1980');
      newPersonPage.personData.placeOfBirth.set('София');
      newPersonPage.personData.country.set('Република България');

      newPersonPage.personAddress.addressType.set('Настоящ адрес');
      newPersonPage.personAddress.valid.set('Да');
      newPersonPage.personAddress.settlement.set('София');
      newPersonPage.personAddress.address.set('Карнобат');
      newPersonPage.personAddress.addressAlt.set('Karnobat');
      newPersonPage.personAddress.postalCode.set('1000');
      newPersonPage.personAddress.phone.set('0999212');


      newPersonPage.personDocumentId.personDocumentIdType.set('Паспорт');
      newPersonPage.personDocumentId.valid.set('Да');
      newPersonPage.personDocumentId.documentNumber.set('D-0001');
      newPersonPage.personDocumentId.documentDateValidFrom.set('10.10.2010');
      newPersonPage.personDocumentId.documentDateValidTo.set('10.10.2013');
      newPersonPage.personDocumentId.bookPageNumber.set('1');
      //expect(newPersonPage.isSaveBtnDisabled).toEqual(true);
      newPersonPage.personDocumentId.documentPublisher.set('Карнобат еър');

      newPersonPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons');

      searchPersonsPage = new SearchPage(ptor);
      expect(searchPersonsPage.datatable.getColumns('lin', 'names', 'uin', 'age')).toEqual([
        ['11232', 'Иван Иванов Иванов', '7005159385', '43'],
        ['12345', 'Атанас Иванов Иванов', '7903245888', '34'],
        ['11111', 'Петър Петров Петров', '6904245664', '44'],
        ['99999', 'Георги Георгиев Георгиев', '9999999999', '34']
      ]);
    });

  });
} (protractor, describe, beforeEach, it, expect, require, xit));