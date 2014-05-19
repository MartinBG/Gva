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

    it('should display initial value for multiple object mode nomenclature', function () {
      expect(nomenclaturePage.multipleObjValNomenclature.get()).toEqual(['Мъж']);
    });

    it('should update model for multiple object mode nomenclature', function () {
      nomenclaturePage.multipleObjValNomenclature.set('Жена');
      expect(nomenclaturePage.multipleObjValNomenclature.get()).toEqual(['Мъж', 'Жена']);
      expect(nomenclaturePage.selectedMultipleObjVal()).toEqual(
        '[{"nomValueId":1,"code":"M","name":"Мъж","nameAlt":"Male","alias":"male"},' +
        '{"nomValueId":2,"code":"W","name":"Жена","nameAlt":"Female","alias":"female"}]');
    });

    it('should display initial value for multiple id mode nomenclature', function () {
      expect(nomenclaturePage.multipleIdValNomenclature.get()).toEqual(['Азербайджан']);
    });

    it('should update model for multiple id mode nomenclature', function () {
      nomenclaturePage.multipleIdValNomenclature.set('Кувейт');
      expect(nomenclaturePage.multipleIdValNomenclature.get()).toEqual(['Азербайджан', 'Кувейт']);
      expect(nomenclaturePage.selectedMultipleIdVal()).toEqual('[6,14]');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
