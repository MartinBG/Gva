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
      var searchInputDatatable1 = gvaBy.datatable('users').inputFilter(),
        firstColumnFirstRow =  gvaBy.datatable('users').row(1).column('username');

      ptor.findElement(searchInputDatatable1).sendKeys('peter');
      ptor.sleep(1000);
      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('peter');

      ptor.findElement(searchInputDatatable1).clear();
      ptor.findElement(searchInputDatatable1).sendKeys('Petrov');
      ptor.sleep(1000);

      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('georgi');
    });

    it('should filter properly with many users loaded', function() {
      var searchInputDatatable2 = gvaBy.datatable('users2').inputFilter(),
        infoTextDatatable2 = gvaBy.datatable('users2').infoText(),
        loadManyBtn = ptor.findElement(protractor.By.id('loadManybtn'));

      loadManyBtn.click().then(function(){
        ptor.findElement(searchInputDatatable2).sendKeys('iztrit');
        ptor.sleep(1000);

        expect(ptor.findElement(infoTextDatatable2).getText())
          .toEqual('Намерени общo 1,024 резултата (от 1 до 1,024) (филтрирани от 4,096 записа)');
      });
    });

    it('should load 4096 users', function() {
      var datatable1InfoText = gvaBy.datatable('users').infoText(),
        datatable2InfoText = gvaBy.datatable('users2').infoText(),
        loadManyBtn = ptor.findElement(protractor.By.id('loadManybtn')),
        infoText1,
        infoText2;

      loadManyBtn.click().then(function(){
        infoText1 = ptor.findElement(datatable1InfoText);
        expect(infoText1.getText()).toEqual('Намерени общo 4,096 резултата (от 1 до 10)');

        infoText2 = ptor.findElement(datatable2InfoText);
        expect(infoText2.getText()).toEqual('Намерени общo 4,096 резултата (от 1 до 4,096)');
      });

    });

    it('should go to edit user page', function() {
      ptor.findElement(gvaBy.datatable('users').row(2).column('buttons')).click();
      var username = ptor.findElement(protractor.By.css('input[ng-model=btnResult]'))
        .getAttribute('value');
      expect(username).toEqual('peter');
    });

    it('should change current page number', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.css('ul[class=pagination] li:nth-child(3) a')).click();
      var infoText = ptor.findElement(gvaBy.datatable('users').infoText());
      expect(infoText.getText()).toContain('11');
    });

    it('should evaluate column content expressions on next page', function () {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.css('ul[class=pagination] li:nth-child(4) a')).click();
      var isActiveCol4 = ptor.findElement(gvaBy.datatable('users').row(4).column('isActive'));
      expect(isActiveCol4.getText()).toBe('Не');
    });

    it('should load 100 entries per page', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(gvaBy.datatable('users').lengthFilter().option(2)).click();

      var infoText = ptor.findElement(gvaBy.datatable('users').infoText());
      expect(infoText.getText()).toEqual('Намерени общo 4,096 резултата (от 1 до 50)');
    });

    it('should evaluate column content expressions when loading more entries', function () {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(gvaBy.datatable('users').lengthFilter().option(2)).click();

      var isActiveCol16 = ptor.findElement(gvaBy.datatable('users').row(16).column('isActive'));
      expect(isActiveCol16.getText()).toBe('Не');
    });

    it('should hide and show columns properly using the button called Columns', function() {
      var buttonHideColumns = ptor.findElement(gvaBy.datatable('users').buttonHideColumns()),
        headers;

      buttonHideColumns.click();

      ptor.findElement(gvaBy.datatable('users').hideColumnsCheckbox(1)).click();
      buttonHideColumns.click();
      ptor.findElement(gvaBy.datatable('users').hideColumnsCheckbox(0)).click();

      ptor.findElement(gvaBy.datatable('users').row(0))
        .findElements(protractor.By.css('th')).then(function(elements){
            headers = protractor.promise.fullyResolved(elements.map(function (el) {
              return el.getText();
            }));
            expect(headers).toEqual(['Роли', 'Активен', '']);
          });

      buttonHideColumns.click();
      ptor.findElement(gvaBy.datatable('users').hideColumnsCheckbox(1)).click();

      headers = [];

      ptor.findElement(gvaBy.datatable('users').row(0))
        .findElements(protractor.By.css('th')).then(function(elements){
            headers = protractor.promise.fullyResolved(elements.map(function (el) {
              return el.getText();
            }));
            expect(headers).toEqual(['Име', 'Роли', 'Активен', '']);
          });
    });

    it('correct sorting settings should be set by sc-datatable parameters', function() {

      ptor.findElement(gvaBy.datatable('users2').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              var sortingSettings = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getAttribute('class');
              }));
              expect(sortingSettings)
                .toEqual(['sorting_disabled sc-username', 'sorting_disabled sc-fullname']);
            });
    });

  });
}(protractor));