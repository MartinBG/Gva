/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Persons search page', function () {
    var ptor = protractor.getInstance(),
        searchBtn,
        newPersonBtn,
        addBtn;

    beforeEach(function () {
      ptor.get('#/persons');

      searchBtn = ptor.findElement(protractor.By.css('div[action=\'search()\'] > button'));
      newPersonBtn = ptor.findElement(protractor.By.css('div[action=\'newPerson()\'] > button'));
      addBtn = ptor.findElement(protractor.By.css('div[action=add] button'));
    });

    it('should display all persons', function () {
      ptor
        .findElements(protractor.By.datatable('persons').column('names'))
        .then(function (elements) {
          var namesPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));

          expect(namesPromise).toEqual([
            'Иван Иванов Иванов',
            'Атанас Иванов Иванов',
            'Петър Петров Петров'
          ]);
        });
    });

    it('should search by lin', function () {
      ptor.findElement(protractor.By.css('div[name=lin] > input')).sendKeys('11111');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Петър Петров Петров');
    });

    it('should search by uin', function () {
      ptor.findElement(protractor.By.css('div[name=uin] > input')).sendKeys('6904245664');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Петър Петров Петров');
    });

    it('should search by names', function () {
      addBtn.click();
      ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
        filters[0].click();
      });
      ptor.findElement(protractor.By.css('div[name=names] > input')).sendKeys('Петър');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Петър Петров Петров');
    });

    it('should search by licences', function () {
      addBtn.click();
      ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
        filters[1].click();
      });
      ptor.findElement(protractor.By.css('div[name=licences] > input')).sendKeys('ATCL');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Иван Иванов Иванов');
    });

    it('should search by ratings', function () {
      addBtn.click();
      ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
        filters[2].click();
      });
      ptor.findElement(protractor.By.css('div[name=ratings] > input')).sendKeys('A 300/310');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Иван Иванов Иванов');
    });

    it('should search by organization', function () {
      addBtn.click();
      ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
        filters[3].click();
      });
      ptor.findElement(protractor.By.css('div[name=organization] > input')).sendKeys('Wizz Air');

      searchBtn.click();
      var names = ptor.findElement(protractor.By.datatable('persons').row(1).column('names'));
      expect(names.getText()).toEqual('Атанас Иванов Иванов');
    });

    it('should redirect to new person page', function () {
      newPersonBtn.click();
      ptor.getCurrentUrl().then(function (url) {
        expect(url).toEqual('http://localhost:52560/#/persons/new');
      });
    });

  });
}(protractor, describe, beforeEach, it, expect));