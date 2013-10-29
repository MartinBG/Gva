/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-float directive', function() {
    var ptor = protractor.getInstance(),
        floatDirectiveElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      floatDirectiveElem = ptor.findElement(protractor.By.name('floatDir'));
    });

    it('should set the text of the element to whatever number is passed.', function() {
      var floatBtnElem = ptor.findElement(protractor.By.name('floatBtn'));

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      floatBtnElem.click();

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('789.12');
      });
    });

    it('should validate user input.', function() {
      floatDirectiveElem.sendKeys('a134asddas56\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      floatDirectiveElem.sendKeys('134.asddas56\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toMatch(/134[\.,]00/);
      });
    });

    it('should format user input.', function() {
      floatDirectiveElem.sendKeys('13345.6\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toMatch(/13[\s,]345[\.,]60/);
      });

      floatDirectiveElem.clear();
      floatDirectiveElem.sendKeys('13345.67\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toMatch(/13[\s,]345[\.,]67/);
      });
    });
  });
}(protractor));