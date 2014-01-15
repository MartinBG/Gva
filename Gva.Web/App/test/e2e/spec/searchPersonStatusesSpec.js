/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Person status search page', function () {
    var ptor = protractor.getInstance(),
      Page = require('../pageObjects/searchPersonStatusesPO'),
      personStatusesPage;

    beforeEach(function () {
      ptor.get('#/persons/1/statuses');
      personStatusesPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personStatusesPage.breadcrumb.getText()).toEqual('Състояния');
    });

    it('should display all person statuses correctly', function () {
      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
        .toEqual([
          ['Негоден', '1', '07.10.1912', '24.12.1912', 'note1', 'Не'],
          ['Майчинство', '2', '04.04.1812', '04.05.1812', 'note2', 'Не'],
          ['Майчинство', '32', '04.11.1922', '15.12.2012', 'note3', 'Не'],
          ['Майчинство', '21', '04.09.2012', '14.05.2812', 'note4', 'Да']
        ]);
    });

    it('should delete a status correctly and stay on the same state', function () {
      var btnSelector = 'tbody tr:first-child td:nth-child(8)';
      ptor.findElement(protractor.By.css(btnSelector)).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      personStatusesPage = new Page(ptor);

      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
        .toEqual([
          ['Майчинство', '2', '04.04.1812', '04.05.1812', 'note2', 'Не'],
          ['Майчинство', '32', '04.11.1922', '15.12.2012', 'note3', 'Не'],
          ['Майчинство', '21', '04.09.2012', '14.05.2812', 'note4', 'Да']
        ]);

    });

    it('should go to edit status page', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(7)';
      ptor.findElement(protractor.By.css(btnSelector)).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses/4');
    });

    it('should go to new status page', function () {
      ptor.findElement(protractor.By.name('createBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses/new');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));