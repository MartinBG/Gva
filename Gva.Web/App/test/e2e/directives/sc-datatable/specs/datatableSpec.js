/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor) {
  'use strict';

  describe('Sc-datatable directive', function() {
    var ptor = protractor.getInstance(),
      gvaBy = require('../../../gva').GvaBy;

    beforeEach(function (){
      ptor.get('#/test/datatable');
    });

    it('should filter properly', function() {
      var searchInputDatatable2 = protractor.By.css(
        'div[ng-model=users2] div[class=dataTables_filter] input'),
        infoTextDatatable2 = protractor.By.css(
          'div[ng-model=users2] div[class=dataTables_info]'),
        searchInputDatatable1 = protractor.By.css(
        'div[ng-model=users] div[class=dataTables_filter] input'),
        firstColumnFirstRow =  gvaBy.datatable('users').row(1).column('username'),
        loadManyBtn = ptor.findElement(protractor.By.id('loadManybtn'));

      ptor.findElement(searchInputDatatable1).sendKeys('peter');
      ptor.sleep(1000);
      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('peter');

      ptor.findElement(searchInputDatatable1).clear();
      ptor.findElement(searchInputDatatable1).sendKeys('Petrov');
      ptor.sleep(1000);

      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('georgi');

      loadManyBtn.click().then(function(){
        ptor.findElement(searchInputDatatable2).sendKeys('iztrit');
        ptor.sleep(1000);

        expect(ptor.findElement(infoTextDatatable2).getText())
          .toMatch(/\S+\s\S+\s1,024\s\S+\s\(\S+\s1\s\S+\s1,024\)\s\(\S+\s\S+\s4,096\s\S+\)/);

      });
    });

    it('should load 4096 users', function() {
      var datatable1InfoText = protractor.By.css(
        'div[ng-model=users] div[class=dataTables_info]'),
        datatable2InfoText = protractor.By.css(
        'div[ng-model=users2] div[class=dataTables_info]'),
        loadManyBtn = ptor.findElement(protractor.By.id('loadManybtn')),
        infoText1,
        infoText2;

      loadManyBtn.click().then(function(){
        infoText1 = ptor.findElement(datatable1InfoText).getText();
        expect(infoText1).toMatch(/\S+\s\S+\s4,096\s\S+\s\(\S+\s1\s\S+\s10\)/);

        infoText2 = ptor.findElement(datatable2InfoText).getText();
        expect(infoText2).toMatch(/\S+\s\S+\s4,096\s\S+\s\(\S+\s1\s\S+\s4,096\)/);
      });

    });

    it('should go to edit user page', function() {
      ptor.findElement(gvaBy.datatable('users').row(2).column('buttons')).click();
      var username = ptor.findElement(protractor.By.name('username'))
        .getAttribute('value');
      expect(username).toEqual('peter');
    });

    it('should change current page number', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.css('ul[class=pagination] li:nth-child(3) a')).click();
      var infoText = ptor.findElement(protractor.By.id('DataTables_Table_0_info')).getText();
      expect(infoText).toContain('11');
    });

    it('should load 100 entries per page', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElements(protractor.By.css('option'))
        .then(function (selectOpts) {
          selectOpts[3].click();
        });
      var infoText = ptor.findElement(protractor.By.id('DataTables_Table_0_info')).getText();
      expect(infoText).toContain('100');
    });

    it('should hide and show columns properly using the button called Columns', function() {
      var dropdownBtn = ptor.findElement(protractor.By.css('button[data-toggle=dropdown]')),
        headers = [],
        checkboxesPromise;

      dropdownBtn.click();
      checkboxesPromise = ptor.findElements(protractor.By.css('input[type=checkbox]'));
      checkboxesPromise.then( function (checkboxElems) {

        checkboxElems[1].click();
        dropdownBtn.click();
        checkboxElems[0].click();

        ptor.findElement(gvaBy.datatable('users').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              headers = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getText();
              }));
              expect(headers).toEqual(['Роли', 'Активен', '']);
            });

        dropdownBtn.click();
        checkboxElems[1].click();

        headers = [];

        ptor.findElement(gvaBy.datatable('users').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              headers = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getText();
              }));
              expect(headers).toEqual(['Име', 'Роли', 'Активен', '']);
            });

      });
    });

    it('correct sorting settings should be set by sc-datatable parameters', function() {

      ptor.findElement(gvaBy.datatable('users2').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              var sortingSettings = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getAttribute('class');
              }));
              expect(sortingSettings)
                .toEqual(['sorting_disabled username', 'sorting_disabled fullname']);
            });

      ptor.get('#/test/datatable/column');
       //no filter displayed
      expect(ptor.findElements(
        protractor.By.css('div[class=dataTables_filter] input')).length)
      .toEqual(undefined);
      //no pagination displayed
      expect(ptor.findElements(
        protractor.By.css('ul[class=pagination] li:nth-child(3) a')).length)
      .toEqual(undefined);
      //no range filter displayed
      expect(ptor.findElements(
        protractor.By.css('option')).length)
      .toEqual(undefined);
      //no dynamic-columns button displayed
      expect(ptor.findElements(
        protractor.By.css('button[data-toggle=dropdown]')).length)
      .toEqual(undefined);
    });

  });
}(protractor));