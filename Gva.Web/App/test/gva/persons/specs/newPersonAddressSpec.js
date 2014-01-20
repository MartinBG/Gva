/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address new page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/personAddressPO'),
        SearchPage = require('../pageObjects/searchPersonAddressPO'),
        newPersonAddressPage,
        searchPersonAddressPage;

    beforeEach(function () {
      ptor.get('#/persons/1/addresses/new');
      newPersonAddressPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newPersonAddressPage.breadcrumb.getText()).toEqual('Нов адрес');
    });

    it('should create new address correctly', function () {
      newPersonAddressPage.addressType.set('Седалище');
      newPersonAddressPage.valid.set('Да');
      newPersonAddressPage.settlement.set('София');
      newPersonAddressPage.address.set('ж.к. Драгалевци');
      newPersonAddressPage.addressAlt.set('j.k.Dragalevci');
      newPersonAddressPage.postalCode.set('1000');
      newPersonAddressPage.phone.set('0999212');

      newPersonAddressPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses');
      searchPersonAddressPage = new SearchPage(ptor);

      expect(searchPersonAddressPage.datatable.getColumns(
          'part_addressType_name',
          'part_settlement_name',
          'part_address',
          'part_postalCode',
          'part_phone',
          'part_valid_name'
          )).toEqual([
          ['Постоянен адрес', 'гр.Пловдив', 'бул.Цариградско шосе 28 ет.9', '', '', 'Не'],
          ['Адрес за кореспонденция', 'гр.Пловдив', 'жг.Толстой бл.39 ап.40', '', '', 'Да'],
          ['Седалище', 'София', 'ж.к. Драгалевци', '1000', '0999212', 'Да']
        ]);
    });

    it('should disable save button unless all required fields are filled out', function() {
      expect(newPersonAddressPage.isSaveBtnDisabled).toEqual(true);

      newPersonAddressPage.addressType.set('Седалище');
      newPersonAddressPage.valid.set('Да');
      newPersonAddressPage.settlement.set('София');
      newPersonAddressPage = new Page(ptor);
      expect(newPersonAddressPage.isSaveBtnDisabled).toEqual(true);

      newPersonAddressPage.address.set('ж.к. Драгалевци');
      newPersonAddressPage.addressAlt.set('j.k.Dragalevci');
      newPersonAddressPage = new Page(ptor);
      expect(newPersonAddressPage.isSaveBtnDisabled).toEqual(false);

      newPersonAddressPage.postalCode.set('1000');
      newPersonAddressPage.phone.set('0999212');
      newPersonAddressPage = new Page(ptor);
      expect(newPersonAddressPage.isSaveBtnDisabled).toEqual(false);
    });

    it('should go to search view at clicking on cancel button', function () {
      newPersonAddressPage.cancel();
      ptor.getCurrentUrl().then(function (url) {
        expect(url).toEqual('http://localhost:52560/#/persons/1/addresses');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));