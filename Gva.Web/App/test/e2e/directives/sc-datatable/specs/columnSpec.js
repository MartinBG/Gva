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
        ptor.findElement(gvaBy.datatable('users').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              var headers = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getText();
              }));
              expect(headers).toEqual(['Потребителско име', 'Роли', 'Активен', '']);
            });
      });

    it('correct sorting settings should be set by the sc-column parameters sorting and sortable',
      function() {
        ptor.findElement(gvaBy.datatable('users').row(0))
          .findElements(protractor.By.css('th')).then(function(elements){
              var sortingSettings = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getAttribute('class');
              }));
              expect(sortingSettings)
                .toEqual([
                  'sorting_disabled username',
                  'roles sorting',
                  'isActive sorting_desc',
                  'sorting'
                ]);
            });
      });

    it('should sort correctly columns data when their headers are clicked', function() {
      var firstUsername,
        username,
        sortValue = ptor.findElement(gvaBy.datatable('users').row(0))
          .findElement(protractor.By.css('th:nth-child(3)'))
          .then(function (element) {
            element.click();
            return element.getAttribute('class');
          });

      firstUsername = ptor.findElement(
          gvaBy.datatable('users').row(1).column('username'));

      expect(firstUsername.getText()).toEqual('test1');

      expect(sortValue).toEqual('isActive sorting_asc');

      ptor.findElement(protractor.By.id('loadManybtn')).click().then(function(){
        username = ptor.findElement(
          gvaBy.datatable('users').row(3070).column('username'));
        expect(username.getText()).toEqual('georgi');

      });
    });

  });
}(protractor));