/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scNomenclature directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/nomenclaturePO'),
        nomenclaturePage;

    beforeEach(function () {
      ptor.get('#/test/nom');
      nomenclaturePage = new Page(ptor);
    });

    it('should display initial value.', function () {
      expect(nomenclaturePage.nomenclature.get()).toEqual('Жена');
      expect(nomenclaturePage.selectedNomId()).toEqual('2');
      expect(nomenclaturePage.selectedNomName()).toEqual('Жена');
    });

    it('should clear selection and update model value.', function () {
      nomenclaturePage.nomenclature.clear();
      expect(nomenclaturePage.selectedNomId()).toEqual('');
      expect(nomenclaturePage.selectedNomName()).toEqual('');
    });

    it('should filter items and update model value.', function () {
      nomenclaturePage.nomenclature.set('Мъж');
      expect(nomenclaturePage.selectedNomId()).toEqual('1');
      expect(nomenclaturePage.selectedNomName()).toEqual('Мъж');
    });

    it('should update on model value changes.', function () {
      nomenclaturePage.changeNomValue();
      expect(nomenclaturePage.nomenclature.get()).toEqual('Неопределен');
    });
    
    it('should display initial value for id mode nomenclatures', function () {
      expect(nomenclaturePage.parentNomenclature.get()).toEqual('Република България');
      expect(nomenclaturePage.childNomenclature.get()).toEqual('София');
    });
    
    it('should clear child nomenclature on parent change', function () {
      nomenclaturePage.parentNomenclature.set('Република Южна Африка');
      expect(nomenclaturePage.childNomenclature.get()).toEqual('');
    });
    
    it('should filter child nomenclature', function () {
      nomenclaturePage.parentNomenclature.set('Republic of Groatia');
      expect(nomenclaturePage.childNomenclature.getDropdownResults())
        .toEqual(['Цавтат', 'Загреб', 'Велика Горица']);
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
