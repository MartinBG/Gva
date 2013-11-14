/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Sc-select directive', function() {
    var ptor = protractor.getInstance(),
        optionsPromise;

    beforeEach(function (){
      ptor.get('#/test/select');

      optionsPromise = ptor.findElements(protractor.By.name('optionsRadios'));
    });

    it('should select the passed option.', function() {
      var selectDirectiveElem = ptor.findElement(protractor.By.name('selectDir'));

      selectDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      optionsPromise.then( function (optionElems) {
        optionElems[2].click().then( function () {
          selectDirectiveElem.getAttribute('value').then(function (value) {
            expect(value).toEqual('2');
          });
        });
      });
    });

    it('should change the model to whatever is selected.', function() {
      var select2Elem = ptor.findElement(protractor.By.className('select2-container'));

      optionsPromise.then(function (optionElems) {
        optionElems[0].getAttribute('checked').then(function (isChecked) {
          expect(isChecked).toBeTruthy();
        });

        select2Elem.click().then( function () {
          ptor.findElements(protractor.By.className('select2-result')).then(function (select2Opts) {
            select2Opts[2].click();
          });

          optionElems[3].getAttribute('checked').then(function (isChecked) {
            expect(isChecked).toBeTruthy();
          });
        });
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));