/*global protractor, describe, beforeEach, it, expect, require, xit*/
(function (protractor, describe, beforeEach, it, expect, xit) {
  'use strict';

  describe('Person address new page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/addresses/addressPO'),
        SearchPage = require('../../pageObjects/addresses/searchAddressPO'),
        newAddressPage,
        searchAddressPage;

    beforeEach(function () {
      ptor.get('#/persons/1/addresses/new');
      newAddressPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newAddressPage.breadcrumb.get()).toEqual('Нов адрес');
    });

    it('should create new address correctly', function () {
      newAddressPage.addressType.set('Настоящ адрес');
      newAddressPage.valid.set('Да');
      newAddressPage.settlement.set('София');
      newAddressPage.address.set('ж.к. Драгалевци');
      newAddressPage.addressAlt.set('j.k.Dragalevci');
      newAddressPage.postalCode.set('1000');
      newAddressPage.phone.set('0999212');

      newAddressPage.save();

      searchAddressPage = new SearchPage(ptor);
      expect(searchAddressPage.breadcrumb.get()).toEqual('Адреси');

      expect(searchAddressPage.datatable.getColumns(
          'part_addressType_name',
          'part_settlement_name',
          'part_address',
          'part_postalCode',
          'part_phone',
          'part_valid_name'
          )).toEqual([
          ['Постоянен адрес', 'гр.Пловдив', 'бул.Цариградско шосе 28 ет.9', '', '', 'Не'],
          ['Адрес за кореспонденция', 'гр.Пловдив', 'жг.Толстой бл.39 ап.40', '', '', 'Да'],
          ['Настоящ адрес', 'София', 'ж.к. Драгалевци', '1000', '0999212', 'Да']
        ]);
    });

    xit('should disable save button unless all required fields are filled out', function() {
      expect(newAddressPage.isSaveBtnDisabled).toEqual(true);

      newAddressPage.addressType.set('Настоящ адрес');
      newAddressPage.valid.set('Да');
      newAddressPage.settlement.set('София');
      newAddressPage = new Page(ptor);
      expect(newAddressPage.isSaveBtnDisabled).toEqual(true);

      newAddressPage.address.set('ж.к. Драгалевци');
      newAddressPage.addressAlt.set('j.k.Dragalevci');
      newAddressPage = new Page(ptor);
      expect(newAddressPage.isSaveBtnDisabled).toEqual(false);

      newAddressPage.postalCode.set('1000');
      newAddressPage.phone.set('0999212');
      newAddressPage = new Page(ptor);
      expect(newAddressPage.isSaveBtnDisabled).toEqual(false);
    });

    it('should go to search view at clicking on cancel button', function () {
      newAddressPage.cancel();

      searchAddressPage = new SearchPage(ptor);
      expect(searchAddressPage.breadcrumb.get()).toEqual('Адреси');
    });
  });

} (protractor, describe, beforeEach, it, expect, xit));