/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scDatatable directive', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/datatablePO'),
        datatablePage;

    beforeEach(function (){
      ptor.get('#/test/datatable');
      datatablePage = new Page(ptor);
    });

    it('should filter properly', function () {
      var searchInputDatatable1 = datatablePage.datatable1.filterInput();
      searchInputDatatable1.sendKeys('peter');
      datatablePage = new Page(ptor);
      expect(datatablePage.datatable1.getColumn('username')).toEqual(['peter']);

      searchInputDatatable1.clear();
      searchInputDatatable1.sendKeys('Petrov');
      datatablePage = new Page(ptor);
      expect(datatablePage.datatable1.getColumn('username')).toEqual(['georgi']);
    });
    
    it('should filter properly with many users loaded', function () {
      datatablePage.loadManyEntriesInTable2();
      datatablePage.datatable2.setFilterInput('iztrit');
      expect(datatablePage.datatable2.getInfoText())
        .toEqual('Намерени общo 1,024 резултата (от 1 до 1,024)');
      expect(datatablePage.datatable2.getFilteredText())
        .toEqual('(филтрирани от 4,096 записа)');
    });

    it('should load 4096 users', function() {
      datatablePage.loadManyEntriesInTable1();
      datatablePage.loadManyEntriesInTable2();
      expect(datatablePage.datatable1.getInfoText())
        .toEqual('Намерени общo 4,096 резултата (от 1 до 10)');

      expect(datatablePage.datatable2.getInfoText())
        .toEqual('Намерени общo 4,096 резултата (от 1 до 4,096)');
    });

    it('should select user', function () {
      datatablePage.datatable1.getRowButtons(2).then(function (buttons) {
        buttons[0].click();
        expect(datatablePage.selectedUser.getAttribute('value')).toEqual('peter');
      });
    });

    it('should change current page number', function() {
      datatablePage.loadManyEntriesInTable1();
      datatablePage.datatable1.goToPage(2);
      expect(datatablePage.datatable1.getInfoText()).toContain('11');
    });
    
    it('should evaluate column content expressions on next page', function () {
      datatablePage.loadManyEntriesInTable1();
      datatablePage.datatable1.goToPage(3);
      datatablePage.datatable1.getColumn('isActive').then(function(results){
        expect(results[3]).toBe('Не');
      });
      
    });

    it('should load 100 entries per page', function() {
      datatablePage.loadManyEntriesInTable1();
      datatablePage.datatable1.setLengthFilterOption(2);
      expect(datatablePage.datatable1.getInfoText())
        .toEqual('Намерени общo 4,096 резултата (от 1 до 50)');
    });
    
    it('should evaluate column content expressions when loading more entries', function () {
      datatablePage.loadManyEntriesInTable1();
      datatablePage.datatable1.setLengthFilterOption(2);
      datatablePage.datatable1.getColumn('isActive').then(function (results) {
        expect(results[15]).toBe('Не');
      });
    });

    it('should hide and show columns properly using the button called Columns', function () {
      datatablePage.datatable1.clickHideColumnsButton();
      datatablePage.datatable1.clickHideColumnCheckbox(1);

      datatablePage.datatable1.clickHideColumnsButton();
      datatablePage.datatable1.clickHideColumnCheckbox(0);
      expect(datatablePage.datatable1.getHeaders()).toEqual(['Роли', 'Активен', '']);

      datatablePage.datatable1.clickHideColumnsButton();
      datatablePage.datatable1.clickHideColumnCheckbox(1);
      expect(datatablePage.datatable1.getHeaders()).toEqual(['Име', 'Роли', 'Активен', '']);
    });

    it('correct sorting settings should be set by sc-datatable parameters', function () {
      datatablePage.datatable2.getColumnsClasses().then(function (columnsClasses) {
        expect(columnsClasses[1]).toMatch(/sorting/);
      });
    });
    
    it('a column should be hidden because of the sc-column parameter called visibility',
      function () {
        expect(datatablePage.datatable3.getHeaders())
               .toEqual(['Потребителско име', 'Роли', 'Активен', '']);
      });

    it('should sort correctly columns data when their headers are clicked', function () {
      datatablePage.datatable3.clickHeader('isActive');
      datatablePage.datatable3.getColumn('username').then(function (results) {
        expect(results[0]).toEqual('test1');
      });
    });

    it('should sort correctly columns data when their headers are clicked' +
      ' and many users are loaded',
      function () {
        datatablePage.loadManyEntriesInTable3().then(function () {
          datatablePage.datatable3.clickHeader('isActive').then(function () {
            expect(datatablePage.datatable3.getRow(3096))
              .toEqual(['peter', 'Role1, Role2', 'Да', 'Редакция']);
          });
        });
      });
   
    it('correct settings should be set by sc-datatable parameters', function () {
      //no filter displayed
      expect(datatablePage.datatable3.isFilterDisplayed()).toEqual(false);
      
      //no pagination displayed
      expect(datatablePage.datatable3.isPaginationDisplayed()).toEqual(false);

      //no range filter displayed
      expect(datatablePage.datatable3.isLengthRangeDisplayed()).toEqual(false);

      //no dynamic-columns button displayed
      expect(datatablePage.datatable3.isHideColumnButtonDisplayed()).toEqual(false);
    });

    it('should display properly dates using column type', function () {
      expect(datatablePage.datatable4.getColumns(
          'documentNumber',
          'documentDateValidFrom',
          'documentDateValidTo'
          )).toEqual([
          ['1', '04.04.2010', '04.08.2010'],
          ['2', '04.06.2010', '07.08.2010'],
          ['3', '02.04.2009', '03.01.2030']
        ]);
    });
  });
}(protractor, describe, beforeEach, it, expect));