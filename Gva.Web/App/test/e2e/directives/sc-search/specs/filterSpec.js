/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-filter directive', function() {
    var ptor = protractor.getInstance();

    beforeEach(function (){
      ptor.get('#/test/search');
    });

    it('should be able to bind to the specified filter name.', function() {
      var filter1 = ptor.findElement(protractor.By.css('div[name=filter1] > input')),
          filter1Input = ptor.findElement(protractor.By.name('filter1Input'));

      filter1Input.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      filter1.sendKeys('filter1');

      filter1Input.getAttribute('value').then(function (value) {
        expect(value).toEqual('filter1');
      });

      filter1Input.sendKeys(' text');

      filter1.getAttribute('value').then(function (value) {
        expect(value).toEqual('filter1 text');
      });
    });

    it('should set the specified classes to the filter.', function () {
      var filter2Dir = ptor.findElement(protractor.By.name('filter2'));

      filter2Dir.getAttribute('class').then(function (className) {
        expect(className).toContain('col-sm-2');
      });
    });

    it('should set default classes to the filter, when no other are specified.', function () {
      var filter3Dir = ptor.findElement(protractor.By.name('filter3'));

      filter3Dir.getAttribute('class').then(function (className) {
        expect(className).toContain('col-sm-3');
      });
    });

    it('should set the filter\'s options.', function () {
      ptor.findElement(protractor.By.name('addBtn')).click().then(function () {
        ptor.findElements(protractor.By.css('div[action=add] li > a'))
            .then(function (nonSelectedFilters) {
          nonSelectedFilters[0].click().then(function () {
            ptor.findElement(protractor.By.css('div[name=filter4] .select2-arrow')).click()
                .then(function () {
              ptor.findElements(protractor.By.css('div.select2-result-label'))
                  .then(function (options) {
                expect(options.length).toBe(2);

                options[0].getText().then(function (text) {
                  expect(text).toEqual('option1');
                });

                options[1].getText().then(function (text) {
                  expect(text).toEqual('option2');
                });
              });
            });
          });
        });
      });
    });
  });
}(protractor));