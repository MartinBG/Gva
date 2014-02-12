/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address edit page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/personAddressPO'),
        SearchPage = require('../pageObjects/searchPersonAddressPO'),
        editPersonAddressPage,
        searchPersonAddressPage;

    beforeEach(function() {
      ptor.get('#/persons/1/addresses/2');
      editPersonAddressPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editPersonAddressPage.breadcrumb.getText()).toEqual('Редакция на адрес');
    });

    it('should display correct filled out data', function () {

      expect(editPersonAddressPage.addressType.get()).toEqual('Постоянен адрес');
      expect(editPersonAddressPage.valid.get()).toEqual('Не');
      expect(editPersonAddressPage.settlement.get()).toEqual('гр.Пловдив');
      expect(editPersonAddressPage.address.get()).toEqual('бул.Цариградско шосе 28 ет.9');
      expect(editPersonAddressPage.addressAlt.get()).toEqual('bul.Tsarigradski shose 28 et.9');

    });
   
    it('should change address data correctly', function () {
      editPersonAddressPage.addressType.set('Настоящ адрес');
      editPersonAddressPage.valid.set('Да');
      editPersonAddressPage.settlement.set('София');
      editPersonAddressPage.address.set('ж.к. Драгалевци');
      editPersonAddressPage.addressAlt.set('j.k.Dragalevci');
      editPersonAddressPage.postalCode.set('1000');
      editPersonAddressPage.phone.set('0999212');

      editPersonAddressPage.save();
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
          ['Настоящ адрес', 'София', 'ж.к. Драгалевци', '1000', '0999212', 'Да'],
          ['Адрес за кореспонденция', 'гр.Пловдив','жг.Толстой бл.39 ап.40', '', '', 'Да']
        ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));