/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scButton directive', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/test/scbutton');
    });

    it('should execute the specified action when the button is clicked.', function () {
      var btnLabel = ptor.findElement(protractor.By.name('btnLabel')),
          btnDirective = ptor.findElement(protractor.By.name('btn'));

      btnLabel.getText().then(function (text) {
        expect(text).toBe('0');
      });

      btnDirective.click().then(function () {
        btnLabel.getText().then(function (text) {
          expect(text).toBe('1');
        });
      });
    });

    it('should show the loading icon when the promise is not null and not resolved.', function () {
      var createBtnElem = ptor.findElement(protractor.By.name('createBtn'));
      var crtBtnPending = ptor.findElement(protractor.By.name('crtBtnPending'));

      createBtnElem.click();

      expect(crtBtnPending.getText()).toBe('true');

    });

    it('should show the resolved icon with the correct title' +
      ' when the promise is resolved.', function () {
      var createBtnElem = ptor.findElement(protractor.By.name('createBtn'));
      var resolveBtnElem = ptor.findElement(protractor.By.name('resolveBtn'));
      var crtBtnPending = ptor.findElement(protractor.By.name('crtBtnPending'));

      createBtnElem.click();
      resolveBtnElem.click();
      
      expect(crtBtnPending.getText()).toBe('');

    });

  });
}(protractor, describe, beforeEach, it, expect));
