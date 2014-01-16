/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scSearch button directive', function() {
    var ptor = protractor.getInstance();

    beforeEach(function (){
      ptor.get('#/test/search');
    });

    it('should execute the specified action whren clicking the button.', function() {
      var btn1Lbl = ptor.findElement(protractor.By.name('btn1ClicksLbl')),
          btn1Dir = ptor.findElement(protractor.By.name('btn1'));

      btn1Lbl.getText().then(function (text) {
        expect(text).toBe('0');
      });

      btn1Dir.click().then(function () {
        btn1Lbl.getText().then(function (text) {
          expect(text).toBe('1');
        });
      });
    });

    it('should set the specified classes to the button.', function () {
      var btn1 = ptor.findElement(protractor.By.css('div[name=btn1] > button'));

      btn1.getAttribute('class').then (function (className) {
        expect(className).toContain('btn-sm');
        expect(className).toContain('btn-info');
      });
    });

    it('should set default classes to the button, when no other are specified.', function () {
      var btn2 = ptor.findElement(protractor.By.css('div[name=btn2] > button'));

      btn2.getAttribute('class').then (function (className) {
        expect(className).toContain('btn-sm');
        expect(className).toContain('btn-default');
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));