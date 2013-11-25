/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor) {
  'use strict';

  describe('Sc-column directive', function() {
    var ptor = protractor.getInstance(),
      gvaBy = require('../../../gva').GvaBy;

    beforeEach(function (){
      ptor.get('#/test/datatable/column');
    });

    it('a column should be hidden because of the sc-column parameter called visibility',
      function() {
        var headerElements,
            headers;

        headerElements =
          ptor.findElement(gvaBy.datatable('users').header())
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

      ptor.findElement(gvaBy.datatable('users').header().column('isActive'))
      .click();

      firstUsername =
        ptor.findElement(gvaBy.datatable('users').row(1).column('username'))
        .getText();

      expect(firstUsername).toEqual('test1');
    });

    it('should sort correctly columns data when their headers are clicked' +
      'and many users are loaded',
      function() {
        var username;

        ptor.findElement(gvaBy.datatable('users').header().column('isActive'))
        .click();
        
        ptor.findElement(protractor.By.id('loadManybtn'))
        .click();

        username =
          ptor.findElement(gvaBy.datatable('users').row(3070).column('username'))
          .getText();

        expect(username).toEqual('georgi');
      });

    it('correct settings should be set by sc-datatable parameters', function() {
       //no filter displayed
      expect(ptor.isElementPresent(gvaBy.datatable('users').filterInput()))
        .toEqual(false);

      //no pagination displayed
      expect(ptor.findElements(
        protractor.By.css('ul[class=pagination] li:nth-child(3) a')).length);

      //no range filter displayed
      expect(ptor.isElementPresent(gvaBy.datatable('users').lengthFilter()))
        .toEqual(false);

      //no dynamic-columns button displayed
      expect(ptor.isElementPresent(gvaBy.datatable('users').hideColumnsButton()))
        .toEqual(false);
    });
  });
}(protractor));