/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Person status search page', function () {
    var ptor = protractor.getInstance(),
      Page = require('../../pageObjects/statuses/searchPersonStatusesPO'),
      EditPage = require('../../pageObjects/statuses/personStatusPO'),
      personStatusesPage,
      editStatusesPage,
      newStatusesPage;

    beforeEach(function () {
      ptor.get('#/persons/1/statuses');
      personStatusesPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personStatusesPage.breadcrumb.get()).toEqual('Състояния');
    });

    it('should display all person statuses correctly', function () {
      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
        .toEqual([
          ['Негоден', '1', '07.10.1912', '24.12.1912', 'Не'],
          ['Майчинство', '2', '04.04.1812', '04.05.1812', 'Не'],
          ['Майчинство', '32', '04.11.1922', '15.12.2012', 'Не'],
          ['Майчинство', '21', '04.09.2012', '14.05.2812', 'Да']
        ]);
    });

    it('should delete a status correctly and stay on the same state', function () {
      personStatusesPage.firstDeleteBtn.click();

      personStatusesPage = new Page(ptor);
      expect(personStatusesPage.breadcrumb.get()).toEqual('Състояния');

      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
        .toEqual([
          ['Майчинство', '2', '04.04.1812', '04.05.1812', 'Не'],
          ['Майчинство', '32', '04.11.1922', '15.12.2012', 'Не'],
          ['Майчинство', '21', '04.09.2012', '14.05.2812', 'Да']
        ]);

    });

    it('should go to edit status page', function() {
      personStatusesPage.firstEditBtn.click();
      editStatusesPage = new EditPage(ptor);
      expect(editStatusesPage.breadcrumb.get()).toEqual('Редакция на състояние');
    });

    it('should go to new status page', function () {
      ptor.findElement(protractor.By.name('createBtn')).click();
      newStatusesPage = new EditPage(ptor);
      expect(newStatusesPage.breadcrumb.get()).toEqual('Ново състояние');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));