/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person address edit page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/addresses/addressPO'),
        SearchPage = require('../../pageObjects/addresses/searchAddressPO'),
        editAddressPage,
        searchAddressPage;

    beforeEach(function() {
      ptor.get('#/persons/1/addresses/2');
      editAddressPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editAddressPage.breadcrumb.getText()).toEqual('Редакция на адрес');
    });

    it('should display correct filled out data', function () {
      expect(editAddressPage.addressType.get()).toEqual('Постоянен адрес');
      expect(editAddressPage.valid.get()).toEqual('Не');
      expect(editAddressPage.settlement.get()).toEqual('гр.Пловдив');
      expect(editAddressPage.address.get()).toEqual('бул.Цариградско шосе 28 ет.9');
      expect(editAddressPage.addressAlt.get()).toEqual('bul.Tsarigradski shose 28 et.9');
    });

    it('should change address data correctly', function () {
      editAddressPage.addressType.set('Настоящ адрес');
      editAddressPage.valid.set('Да');
      editAddressPage.settlement.set('София');
      editAddressPage.address.set('ж.к. Драгалевци');
      editAddressPage.addressAlt.set('j.k.Dragalevci');
      editAddressPage.postalCode.set('1000');
      editAddressPage.phone.set('0999212');

      editAddressPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses');
      searchAddressPage = new SearchPage(ptor);

      expect(searchAddressPage.datatable.getColumns(
          'part_addressType_name',
          'part_settlement_name',
          'part_address',
          'part_postalCode',
          'part_phone',
          'part_valid_name'
          )).toEqual([
          ['Настоящ адрес', 'София', 'ж.к. Драгалевци', '1000', '0999212', 'Да'],
          ['Адрес за кореспонденция', 'гр.Пловдив','жг.Толстой бл.39 ап.40', '', '', 'Да']
        ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));