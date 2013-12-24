﻿/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person status edit page', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/persons/1/statuses/4');
    });

    it('should update breadcrumb text', function () {
        /*jshint quotmark:false */
        var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
          .getText();
        expect(text).toEqual('Редакция на състояние');
      });

    it('should display status data correctly', function () {

      expect(ptor.findElement(protractor.By.nomenclature('model.personStatusType')
        .text()).getText()).toEqual('Негоден');

      ptor.findElement(protractor.By.input('model.documentNumber'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('1');
        });

      ptor.findElement(protractor.By.css('div[name=docDateValidFrom] > input'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('07.10.1912');
        });

      ptor.findElement(protractor.By.css('div[name=docDateValidTo] > input'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('24.12.1912');
        });

      ptor.findElement(protractor.By.input('model.notes'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('note1');
        });

    });

    it('should fill status data correctly', function () {
      ptor.findElement(protractor.By.nomenclature('model.personStatusType'))
        .click();
      ptor.findElement(protractor.By.nomenclature('model.personStatusType').dropdownInput())
        .sendKeys('Майчинство');
      ptor.findElement(protractor.By.nomenclature('model.personStatusType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      var docNumInput = ptor.findElement(protractor.By.input('model.documentNumber'));
      docNumInput.clear();
      docNumInput.sendKeys('123');

      var docDateValidFromInput =
        ptor.findElement(protractor.By.css('div[name=docDateValidFrom] > input'));
      docDateValidFromInput.clear();
      docDateValidFromInput.sendKeys('22.01.2012');

      var docDateValidToInput =
        ptor.findElement(protractor.By.css('div[name=docDateValidTo] > input'));
      docDateValidToInput.clear();
      docDateValidToInput.sendKeys('22.12.2014');

      var notesInput = ptor.findElement(protractor.By.input('model.notes'));
      notesInput.clear();
      notesInput.sendKeys('test');

      ptor.findElement(protractor.By.name('saveBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      var dataPromise;

      ptor
        .findElements(protractor.By.datatable('statuses')
        .column('part.personStatusType.name')).then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
              return el.getText();
            }));
   
          expect(dataPromise).toEqual(['Майчинство', 'Майчинство', 'Майчинство', 'Майчинство']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentNumber'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['123', '2', '32', '21']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidFrom'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['22.01.2012', '04.04.1812', '04.11.1922', '04.09.2012']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidTo'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['22.12.2014', '04.05.1812', '15.12.2012', '14.05.2812']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.notes'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['test', 'note2', 'note3', 'note4']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.isActive'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['Да', 'Не', 'Не', 'Да']);
        });

    });

    it('should cancel editing correctly', function () {
      ptor.get('#/persons/1/statuses/1');

      ptor.findElement(protractor.By.name('cancelBtn')).click().then(function () {
        ptor.getCurrentUrl().then(function (url) {
          expect(url).toEqual('http://localhost:52560/#/persons/1/statuses');
        });
      });

      var dataPromise;

      ptor
        .findElements(protractor.By.datatable('statuses')
        .column('part.personStatusType.name')).then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['Негоден', 'Майчинство', 'Майчинство', 'Майчинство']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentNumber'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['1', '2', '32', '21']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidFrom'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['07.10.1912', '04.04.1812', '04.11.1922', '04.09.2012']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidTo'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['24.12.1912', '04.05.1812', '15.12.2012', '14.05.2812']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.notes'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['note1', 'note2', 'note3', 'note4']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.isActive'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['Не', 'Не', 'Не', 'Да']);
        });

    });

  });
}(protractor, describe, beforeEach, it, expect));