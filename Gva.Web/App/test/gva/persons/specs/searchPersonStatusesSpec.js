/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person status search page', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/persons/1/statuses');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Състояния');
    });

    it('should display data correctly', function () {
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

    
    it('should delete a status correctly', function () {
      var btnSelector = 'tbody tr:first-child td:nth-child(8)';
      ptor.findElement(protractor.By.css(btnSelector)).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      var dataPromise;

      ptor
        .findElements(protractor.By.datatable('statuses')
        .column('part.personStatusType.name')).then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['Майчинство', 'Майчинство', 'Майчинство']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentNumber'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['2', '32', '21']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidFrom'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['04.04.1812', '04.11.1922', '04.09.2012']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.documentDateValidTo'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['04.05.1812', '15.12.2012', '14.05.2812']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.notes'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['note2', 'note3', 'note4']);
        });

      ptor
        .findElements(protractor.By.datatable('statuses').column('part.isActive'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(dataPromise).toEqual(['Не', 'Не', 'Да']);
        });

    });

    it('should go to edit status page', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(7)';
      ptor.findElement(protractor.By.css(btnSelector)).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses/4');
    });

    it('should go to new status page', function () {
      ptor.findElement(protractor.By.name('createBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses/new');
    });

  });
}(protractor, describe, beforeEach, it, expect));