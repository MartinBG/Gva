/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scDate directive', function() {
    var ptor = protractor.getInstance(),
        dateInputElem,
        dateLabelElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      dateInputElem = ptor.findElement(protractor.By.css('div.date > input'));
      dateLabelElem = ptor.findElement(protractor.By.name('dateLbl'));
    });

    it('should set the date to whatever date is passed.', function() {
      var dateBtnElem = ptor.findElement(protractor.By.name('dateBtn'));

      dateInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      dateBtnElem.click();

      dateInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('10.08.1990');
      });
    });

    it('should change the model to whatever date is typed.', function() {
      dateLabelElem.getText().then(function (value) {
        expect(value).toEqual('');
      });

      dateInputElem.sendKeys('12.10.2013\t');

      dateLabelElem.getText().then(function (value) {
        expect(value).toEqual('2013-10-12T00:00:00');
      });
    });

    it('should allow clearing the date.', function () {
      dateInputElem.sendKeys('12.10.2013\t');
      dateInputElem.clear();

      dateLabelElem.getText().then(function (text) {
        expect(text).toEqual('');
      });
    });

    it('should change the model to whatever date is selected.', function() {
      var dateLabelElem = ptor.findElement(protractor.By.name('dateLbl'));

      ptor.findElement(protractor.By.className('glyphicon-calendar')).click().then( function () {
        ptor.findElements(protractor.By.className('day')).then (function (dayElems) {
          dayElems[15].click().then(function () {
            dateInputElem.getAttribute('value').then(function (datepickerValue) {
              var dateArr = datepickerValue.split('.'),
                  day = dateArr[0],
                  month = dateArr[1],
                  year = dateArr[2],
                  ISOdate = year + '-' + month + '-' + day +'T00:00:00';

              dateLabelElem.getText().then(function (modelValue) {
                expect(modelValue).toEqual(ISOdate);
              });
            });
          });
        });
      });
    });

    it('should validate user input.', function() {
      dateInputElem.sendKeys('alabala\t');

      dateInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      dateLabelElem.getText().then(function (text) {
        expect(text).toEqual('');
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));