/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-filter directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/searchPO'),
        searchPage;

    beforeEach(function (){
      ptor.get('#/test/search');

      searchPage = new Page(ptor);
    });

    it('should be able to bind to the specified filter name.', function() {
      expect(searchPage.getInput('filter1Input')).toBe('');

      searchPage.searchForm.setFilter('filter1', 'filter1');
      expect(searchPage.getInput('filter1Input')).toBe('filter1');

      searchPage.searchForm.setFilter('filter1', ' text');
      expect(searchPage.getInput('filter1Input')).toBe('filter1 text');
    });

    it('should set the specified classes to the filter.', function () {
      expect(searchPage.searchForm.getFilterContainer('filter2').getAttribute('class'))
        .toContain('col-sm-2');
    });

    it('should set default classes to the filter, when no other are specified.', function () {
      expect(searchPage.searchForm.getFilterContainer('filter3').getAttribute('class'))
        .toContain('col-sm-3');
    });

    it('should set the filter\'s options.', function () {
      searchPage.searchForm.addFilter('filter4');

      expect(searchPage.searchForm.getFilterOptions('filter4')).toEqual(['option1', 'option2']);
    });
  });
}(protractor, describe, beforeEach, it, expect, require));