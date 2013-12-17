/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Sc-datatable directive', function() {
    var ptor = protractor.getInstance();

    beforeEach(function (){
      ptor.get('#/test/datatable');
    });

    it('should filter properly', function() {
      var searchInputDatatable1 = protractor.By.datatable('users').filterInput(),
        firstColumnFirstRow =  protractor.By.datatable('users').row(1).column('username');

      ptor.findElement(searchInputDatatable1).sendKeys('peter');

      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('peter');

      ptor.findElement(searchInputDatatable1).clear();
      ptor.findElement(searchInputDatatable1).sendKeys('Petrov');

      expect(ptor.findElement(firstColumnFirstRow).getText()).toEqual('georgi');
    });

    it('should filter properly with many users loaded', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.datatable('users2').filterInput()).sendKeys('iztrit');

      expect(ptor.findElement(protractor.By.datatable('users2').infoText()).getText())
        .toEqual('Намерени общo 1,024 резултата (от 1 до 1,024) (филтрирани от 4,096 записа)');
    });

    it('should load 4096 users', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();

      expect(ptor.findElement(protractor.By.datatable('users').infoText()).getText())
        .toEqual('Намерени общo 4,096 резултата (от 1 до 10)');

      expect(ptor.findElement(protractor.By.datatable('users2').infoText()).getText())
        .toEqual('Намерени общo 4,096 резултата (от 1 до 4,096)');
    });

    it('should select user', function() {
      ptor.findElement(protractor.By.datatable('users').row(2).column('buttons')).click();

      var selectedUser =
        ptor.findElement(protractor.By.model('selectedUser')).getAttribute('value');
      expect(selectedUser).toEqual('peter');
    });

    it('should change current page number', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.datatable('users').pageButton(2)).click();

      var infoText = ptor.findElement(protractor.By.datatable('users').infoText());
      expect(infoText.getText()).toContain('11');
    });

    it('should evaluate column content expressions on next page', function () {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.datatable('users').pageButton(3)).click();
      var isActiveCol4 =
        ptor.findElement(protractor.By.datatable('users').row(4).column('isActive'));
      expect(isActiveCol4.getText()).toBe('Не');
    });

    it('should load 100 entries per page', function() {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.datatable('users').lengthFilter().option(2)).click();

      var infoText = ptor.findElement(protractor.By.datatable('users').infoText());
      expect(infoText.getText()).toEqual('Намерени общo 4,096 резултата (от 1 до 50)');
    });

    it('should evaluate column content expressions when loading more entries', function () {
      ptor.findElement(protractor.By.id('loadManybtn')).click();
      ptor.findElement(protractor.By.datatable('users').lengthFilter().option(2)).click();

      var isActiveCol16 =
        ptor.findElement(protractor.By.datatable('users').row(16).column('isActive'));
      expect(isActiveCol16.getText()).toBe('Не');
    });

    it('should hide and show columns properly using the button called Columns', function() {
      var hideColumnsButton =
            ptor.findElement(protractor.By.datatable('users').hideColumnsButton()),
          headerElements,
          headers;

      hideColumnsButton.click();
      ptor.findElement(protractor.By.datatable('users').hideColumnCheckbox(1)).click();

      hideColumnsButton.click();
      ptor.findElement(protractor.By.datatable('users').hideColumnCheckbox(0)).click();

      headerElements =
        ptor.findElement(protractor.By.datatable('users').header())
        .findElements(protractor.By.css('th'));

      headers = headerElements.then(function (he) {
        return protractor.promise.fullyResolved(he.map(function (el) {
          return el.getText();
        }));
      });
      expect(headers).toEqual(['Роли', 'Активен', '']);

      hideColumnsButton.click();
      ptor.findElement(protractor.By.datatable('users').hideColumnCheckbox(1)).click();

      headerElements =
        ptor.findElement(protractor.By.datatable('users').header())
        .findElements(protractor.By.css('th'));

      headers = headerElements.then(function (he) {
        return protractor.promise.fullyResolved(he.map(function (el) {
          return el.getText();
        }));
      });

      expect(headers).toEqual(['Име', 'Роли', 'Активен', '']);
    });

    it('correct sorting settings should be set by sc-datatable parameters', function() {
      ptor.findElement(protractor.By.datatable('users2').header())
      .findElements(protractor.By.css('th'))
      .then(function(elements) {
        return protractor.promise.fullyResolved(elements.map(function (el) {
          return el.getAttribute('class');
        }));
      })
      .then(function (sortingSettings) {
        expect(sortingSettings)
          .toEqual(['sorting_disabled scdt-username', 'sorting_disabled scdt-fullname']);
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));