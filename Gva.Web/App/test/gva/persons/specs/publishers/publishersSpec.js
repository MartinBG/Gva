/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Choose publishers page', function () {
    var ptor = protractor.getInstance(),
    Page = require('../../pageObjects/publishers/publishersPO'),
    ParentPage = require('../../pageObjects/documentTrainings/trainingPO'),
    publishersPage,
    parentPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentTrainings/new/choosepublisher');
      publishersPage = new Page(ptor);
    });

    it('should display publishers', function () {
      expect(publishersPage.datatable.getColumns('name')).toEqual([
        ['AAK Progres'],
        ['Wizz Air'],
        ['Fly Emirates'],
        ['Training Flight Operations Support and Services'],
        ['Bombardier Aerospace Training Center'],
        ['Поделение'],
        ['SOFIA FLIGHT TRAINING CENTER'],
        ['МВР'],
        ['ST Aerospase'],
        ['Japan Civil Aviation Authority']
      ]);
    });

    it('should search', function () {
      publishersPage.searchForm.setFilter('text', 'AAK');
      publishersPage.searchForm.clickButton('search');

      publishersPage = new Page(ptor);
      expect(publishersPage.datatable.getColumn('name')).toEqual(['AAK Progres']);
    });

    it('should redirect to new person page', function () {
      publishersPage.searchForm.clickButton('goBack');
      parentPage = new ParentPage(ptor);
      expect(parentPage.breadcrumb.get()).toEqual('Ново обучение');
    });

    it('should select publisher and pass it to parent', function () {
      publishersPage.firstSelectBtn.click();
      parentPage = new ParentPage(ptor);
      expect(parentPage.breadcrumb.get()).toEqual('Ново обучение');
      parentPage.staffType.set('Общ документ');
      expect(parentPage.documentPublisher.get())
        .toEqual('AAK Progres');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));