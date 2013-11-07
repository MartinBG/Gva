/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-column directive', function() {
    var ptor = protractor.getInstance();

    beforeEach(function (){
      ptor.get('#/test/datatable/column');
    });

    it('a column should be hidden because of the sc-column parameter called visibility',
      function() {
        var usernames = [];
        ptor.findElements(protractor.By.css('thead tr th'))
        .then(function (elements) {
          usernames = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
          expect(usernames).toEqual(['Потребителско име', 'Роли', 'Активен', '']);
        });
      });

    it('correct sorting settings should be set by the sc-column parameters sorting and sortable',
      function() {
        var sortingSettings = [];
        ptor.findElements(protractor.By.css('thead tr th'))
            .then(function (elements) {
              sortingSettings = protractor.promise.fullyResolved(elements.map(function (el) {
                return el.getAttribute('class');
              }));
              expect(sortingSettings)
                .toEqual(['sorting_disabled', 'sorting', 'sorting_desc', 'sorting']);
            });
      });

    it('should sort correctly columns data when their headers are clicked', function() {
      var firstUsername,
        username,
        sortValue = ptor.findElement(protractor.By.css('thead tr th:nth-child(3)'))
          .then(function (element) {
            element.click();
            return element.getAttribute('class');
          });
      firstUsername = ptor.findElement(protractor.By.css('tbody tr td:first-child'));
      firstUsername.getText().then(function(text) {
        expect(text).toEqual('test1');
      });
      expect(sortValue).toEqual('sorting_asc');

      ptor.findElement(protractor.By.id('loadManybtn')).click().then(function(){
        username = ptor.findElement(protractor.By.css('tbody tr:nth-child(3070) td:first-child'));
        username.getText().then(function(text) {
          expect(text).toEqual('georgi');
        });
      });
    });

  });
}(protractor));