/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-search directive', function() {
    var ptor = protractor.getInstance(),
        Page = require( '../pageObjects/searchPO' ),
        searchPage;

    beforeEach(function (){
      ptor.get('#/test/search');

      searchPage = new Page(ptor);
    });

    it('should hide the filters not specified in the "selected-filters" attribute.',
        function () {
      expect(searchPage.searchForm.getVisibleFilters())
         .toEqual(['filter1', 'filter2', 'filter3']);
    });

    it('should allow the user to add filter that is not selected.', function() {
      expect(searchPage.searchForm.getVisibleFilters())
         .toEqual(['filter1', 'filter2', 'filter3']);

      searchPage.searchForm.addFilter('filter4');

      expect(searchPage.searchForm.getVisibleFilters())
        .toEqual(['filter1', 'filter2', 'filter3', 'filter4']);
    });

    it('should hide the add button when all filters are selected.', function () {
      searchPage.searchForm.addFilter('filter4');

      expect(searchPage.searchForm.getButton('addBtn').isDisplayed()).toBe(false);
    });

    it('should allow the user to remove filters.', function () {
      expect(searchPage.searchForm.getVisibleFilters())
         .toEqual(['filter1', 'filter2', 'filter3']);

      searchPage.searchForm.removeFilter('filter2');

      expect(searchPage.searchForm.getVisibleFilters())
          .toEqual(['filter1', 'filter3']);
    });

    it('should allow the user to add back removed filter with label.', function () {
      searchPage.searchForm.removeFilter('filter2');

      expect(searchPage.searchForm.getVisibleFilters())
          .toEqual(['filter1', 'filter3']);

      searchPage.searchForm.addFilter('filter2');

      expect(searchPage.searchForm.getVisibleFilters())
          .toEqual(['filter1', 'filter2', 'filter3']);
    });

    it('should not allow the user to add again removed filter without label.', function () {
      searchPage.searchForm.removeFilter('filter3');
      searchPage.searchForm.addFilter('filter4');
      expect(searchPage.searchForm.getButton('addBtn').isDisplayed()).toBe(false);
    });

    it('should change the filters object to contain only the selected filters.', function () {
      expect(searchPage.getVisibleInputs())
        .toEqual(['filter1Input', 'filter2Input', 'filter3Input']);

      searchPage.searchForm.removeFilter('filter2');
      expect(searchPage.getVisibleInputs()).toEqual(['filter1Input', 'filter3Input']);

      searchPage.searchForm.addFilter('filter4');
      expect(searchPage.getVisibleInputs())
        .toEqual(['filter1Input', 'filter3Input', 'filter4Input']);
    });
  });
}(protractor, describe, beforeEach, it, expect, require));