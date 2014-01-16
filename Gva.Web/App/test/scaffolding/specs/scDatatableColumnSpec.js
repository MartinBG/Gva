/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scDatatable column directive', function() {
    var ptor = protractor.getInstance();

    beforeEach(function (){
      ptor.get('#/test/column');
    });

    it('a column should be hidden because of the sc-column parameter called visibility',
      function() {
        var headerElements,
            headers;

        headerElements =
          ptor.findElement(protractor.By.datatable('users').header())
          .findElements(protractor.By.css('th'));

        headers = headerElements.then(function (he) {
          return protractor.promise.fullyResolved(he.map(function (el) {
            return el.getText();
          }));
        });

        expect(headers).toEqual(['Потребителско име', 'Роли', 'Активен', '']);
      });

    it('should sort correctly columns data when their headers are clicked', function() {
      var firstUsername;

      ptor.findElement(protractor.By.datatable('users').header().column('isActive'))
      .click();

      firstUsername =
        ptor.findElement(protractor.By.datatable('users').row(1).column('username'))
        .getText();

      expect(firstUsername).toEqual('test1');
    });

    it('should sort correctly columns data when their headers are clicked' +
      'and many users are loaded',
      function() {
        var username;

        ptor.findElement(protractor.By.datatable('users').header().column('isActive'))
        .click();
        
        ptor.findElement(protractor.By.id('loadManybtn'))
        .click();

        username =
          ptor.findElement(protractor.By.datatable('users').row(3070).column('username'))
          .getText();

        expect(username).toEqual('georgi');
      });

    it('correct settings should be set by sc-datatable parameters', function() {
      //no filter displayed
      expect(ptor.isElementPresent(protractor.By.datatable('users').filterInput()))
        .toEqual(false);

      //no pagination displayed
      expect(ptor.isElementPresent(protractor.By.css('ul[class=pagination]')))
        .toEqual(false);

      //no range filter displayed
      expect(ptor.isElementPresent(protractor.By.datatable('users').lengthFilter()))
        .toEqual(false);

      //no dynamic-columns button displayed
      expect(ptor.findElement(protractor.By.datatable('users').hideColumnsButton()).isDisplayed())
        .toEqual(false);
    });
  });
}(protractor, describe, beforeEach, it, expect));