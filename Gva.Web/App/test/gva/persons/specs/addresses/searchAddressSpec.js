/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address search page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/addresses/searchAddressPO'),
        personAddressesPage;

    beforeEach(function() {
      ptor.get('#/persons/1/addresses');
      personAddressesPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personAddressesPage.breadcrumb.getText()).toEqual('Адреси');
    });

    it('should display data correctly', function () {
      expect(personAddressesPage.datatable.getColumns(
          'part_addressType_name',
          'part_settlement_name',
          'part_address',
          'part_valid_name'
          )).toEqual([
          ['Постоянен адрес', 'гр.Пловдив', 'бул.Цариградско шосе 28 ет.9', 'Не'],
          ['Адрес за кореспонденция', 'гр.Пловдив', 'жг.Толстой бл.39 ап.40', 'Да' ]
        ]);
    });
     
    it('should delete an address', function () {
      personAddressesPage.firstDeleteBtn.click();
      personAddressesPage = new Page(ptor);
      expect(personAddressesPage.datatable.getColumns(
          'part_addressType_name',
          'part_settlement_name',
          'part_address',
          'part_valid_name'
          )).toEqual([
        ['Адрес за кореспонденция', 'гр.Пловдив', 'жг.Толстой бл.39 ап.40', 'Да']
      ]);
    });

    it('should go to edit page', function() {
      personAddressesPage.firstEditBtn.click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses/2');
    });
  });

} (protractor, describe, beforeEach, it, expect));