﻿/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  var gvaBy = require('../../../gva.js').GvaBy;

  describe('Sc-select directive', function() {
    var ptor = protractor.getInstance(),
        selectedNomId,
        selectedNomName;

    beforeEach(function () {
      ptor.get('#/test/nom');
      selectedNomId = ptor.findElement(protractor.By.id('selectedNomId'));
      selectedNomName = ptor.findElement(protractor.By.id('selectedNomName'));
    });

    it('should display initial value.', function() {
      expect(ptor.findElement(gvaBy.nomenclature('gender').text()).getText()).toEqual('Жена');
      expect(selectedNomId.getText()).toEqual('2');
      expect(selectedNomName.getText()).toEqual('Жена');
    });

    it('should clear selection and update model value.', function() {
      ptor
        .findElement(gvaBy.nomenclature('gender').deselectButton())
        .click()
        .then(function () {
          expect(selectedNomId.getText()).toEqual('');
          expect(selectedNomName.getText()).toEqual('');
        });
    });

    it('should filter items and update model value.', function() {
      ptor
        .findElement(gvaBy.nomenclature('gender'))
        .click()
        .then(function () {
          return ptor
            .findElement(gvaBy.nomenclature('gender').dropdownInput())
            .sendKeys('Мъж');
        })
        .then(function () {
          return ptor
            .findElement(gvaBy.nomenclature('gender').dropdownInput())
            .sendKeys(protractor.Key.ENTER);
        })
        .then(function () {
          expect(selectedNomId.getText()).toEqual('1');
          expect(selectedNomName.getText()).toEqual('Мъж');
        });
    });

    it('should update on model value changes.', function() {
      ptor
        .findElement(protractor.By.id('changeBtn'))
        .click()
        .then(function () {
          expect(selectedNomId.getText()).toEqual('3');
          expect(selectedNomName.getText()).toEqual('Неопределен');
        });
    });
  });
}(protractor, describe, beforeEach, it, expect, require));