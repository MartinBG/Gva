/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scSearch directive', function () {
    var ptor = protractor.getInstance(),
        addBtn;

    beforeEach(function (){
      ptor.get('#/test/search');

      addBtn = ptor.findElement(protractor.By.name('addBtn'));
    });

    it('should hide the filters not specified in the "selected-filters" attribute.',
        function() {
      ptor.findElements(protractor.By.css('form[name=search] div.form-group'))
          .then(function (filters) {
        expect(filters.length).toBe(4);

        filters[0].getAttribute('name').then(function (name) {
          expect(name).toBe('filter1');
        });

        filters[1].getAttribute('name').then(function (name) {
          expect(name).toBe('filter2');
        });

        filters[2].getAttribute('name').then(function (name) {
          expect(name).toBe('filter3');
        });

        filters[3].getAttribute('name').then(function (name) {
          expect(name).toBe('filter4');
        });
      });

      ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
          .then(function (hiddenFilters) {
        expect(hiddenFilters.length).toBe(1);

        hiddenFilters[0].getAttribute('name').then(function (name) {
          expect(name).toBe('filter4');
        });
      });
    });

    it('should allow the user to add filter that is not selected.', function() {
      ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
          .then(function (hiddenFilters) {
        expect(hiddenFilters.length).toBe(1);

        hiddenFilters[0].getAttribute('name').then(function (name) {
          expect(name).toBe('filter4');
        });
      });

      addBtn.click().then(function () {
        ptor.findElements(protractor.By.css('div[action=add] li > a'))
            .then(function (nonSelectedFilters) {
          expect(nonSelectedFilters.length).toBe(1);

          nonSelectedFilters[0].getText().then (function (value) {
            expect(value).toBe('Filter 4');
          });

          nonSelectedFilters[0].click().then(function () {
            ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
                .then(function (hiddenFilters) {
              expect(hiddenFilters.length).toBe(0);
            });
          });
        });
      });
    });

    it('should hide the add button when all filters are selected.', function () {
      addBtn.click().then(function () {
        ptor.findElement(protractor.By.css('div[action=add] li')).click().then(function () {
          ptor.findElement(protractor.By.css('div[action=add] > div')).getAttribute('class')
              .then(function (className) {
            expect(className).toContain('ng-hide');
          });
        });
      });
    });

    it('should allow the user to remove filters.', function () {
      ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
          .then(function (hiddenFilters) {
        expect(hiddenFilters.length).toBe(1);

        hiddenFilters[0].getAttribute('name').then(function (name) {
          expect(name).toBe('filter4');
        });
      });

      ptor.findElement(protractor.By.css('div[name=filter2] > button')).click().then(function () {
        ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
            .then(function (hiddenFilters) {
          expect(hiddenFilters.length).toBe(2);

          hiddenFilters[0].getAttribute('name').then(function (name) {
            expect(name).toBe('filter2');
          });

          hiddenFilters[1].getAttribute('name').then(function (name) {
            expect(name).toBe('filter4');
          });
        });
      });
    });

    it('should allow the user to add back removed filter with label.', function () {
      ptor.findElement(protractor.By.css('div[name=filter2] > button')).click().then(function () {
        addBtn.click().then(function () {
          ptor.findElements(protractor.By.css('div[action=add] li > a'))
              .then(function (nonSelectedFilters) {
            expect(nonSelectedFilters.length).toBe(2);

            nonSelectedFilters[0].getText().then (function (value) {
              expect(value).toBe('Filter 2');
            });

            nonSelectedFilters[1].getText().then (function (value) {
              expect(value).toBe('Filter 4');
            });

            nonSelectedFilters[0].click().then(function () {
              ptor.findElements(protractor.By.css('form div.form-group[class~=ng-hide]'))
                  .then(function (hiddenFilters) {
                expect(hiddenFilters.length).toBe(1);

                hiddenFilters[0].getAttribute('name').then(function (name) {
                  expect(name).toBe('filter4');
                });
              });
            });
          });
        });
      });
    });

    it('should not allow the user to add again removed filter without label.', function () {
      ptor.findElement(protractor.By.css('div[name=filter3] > button')).click().then(function () {
        addBtn.click().then(function () {
          ptor.findElements(protractor.By.css('div[action=add] li > a'))
              .then(function (nonSelectedFilters) {
            expect(nonSelectedFilters.length).toBe(1);

            nonSelectedFilters[0].getText().then (function (value) {
              expect(value).toBe('Filter 4');
            });
          });
        });
      });
    });

    it('should change the filters object to contain only the selected filters.', function () {
      ptor.findElements(protractor.By.css('form[name=modelForm] div.form-group > input'))
          .then(function (selectedFilters) {
        expect(selectedFilters.length).toBe(3);

        selectedFilters[0].getAttribute('name').then(function (name) {
          expect(name).toEqual('filter1Input');
        });

        selectedFilters[1].getAttribute('name').then(function (name) {
          expect(name).toEqual('filter2Input');
        });

        selectedFilters[2].getAttribute('name').then(function (name) {
          expect(name).toEqual('filter3Input');
        });
      });

      ptor.findElement(protractor.By.css('div[name=filter2] > button')).click().then(function () {
        ptor.findElements(protractor.By.css('form[name=modelForm] div.form-group > input'))
          .then(function (selectedFilters) {
          expect(selectedFilters.length).toBe(2);

          selectedFilters[0].getAttribute('name').then(function (name) {
            expect(name).toEqual('filter1Input');
          });

          selectedFilters[1].getAttribute('name').then(function (name) {
            expect(name).toEqual('filter3Input');
          });
        });
      });

      addBtn.click().then(function () {
        ptor.findElements(protractor.By.css('div[action=add] li > a'))
            .then(function (nonSelectedFilters) {
          nonSelectedFilters[1].click().then(function () {
            ptor.findElements(protractor.By.css('form[name=modelForm] div.form-group > input'))
                .then(function (selectedFilters) {
              expect(selectedFilters.length).toBe(3);

              selectedFilters[0].getAttribute('name').then(function (name) {
                expect(name).toEqual('filter1Input');
              });

              selectedFilters[1].getAttribute('name').then(function (name) {
                expect(name).toEqual('filter3Input');
              });

              selectedFilters[2].getAttribute('name').then(function (name) {
                expect(name).toEqual('filter4Input');
              });
            });
          });
        });
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));