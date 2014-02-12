/*global protractor, describe, beforeEach, it, expect, require, xit*/
(function (protractor, describe, beforeEach, it, expect, require, xit) {
  'use strict';

  describe('Person status new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/personStatusPO'),
        SearchPage = require('../pageObjects/searchPersonStatusesPO'),
        newPersonStatusPage,
        personStatusesPage;

    beforeEach(function () {
      ptor.get('#/persons/1/statuses/new');
      newPersonStatusPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newPersonStatusPage.breadcrumb.getText()).toEqual('Ново състояние');
    });

    xit('should disable the save button when requried fields are not filled', function () {
      expect(newPersonStatusPage.saveBtn.getAttribute('disabled')).toBe('true');
    });

    it('should fill and save status data correctly', function () {
      newPersonStatusPage.personStatusType.set('Майчинство');
      newPersonStatusPage.documentNumber.set('999');
      newPersonStatusPage.documentDateValidFrom.set('22.01.2222');
      newPersonStatusPage.documentDateValidTo.set('22.12.2222');
      newPersonStatusPage.notes.set('test123');
      newPersonStatusPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      personStatusesPage = new SearchPage(ptor);

      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
      .toEqual([
        ['Негоден', '1', '07.10.1912', '24.12.1912', 'Не'],
        ['Майчинство', '2', '04.04.1812', '04.05.1812', 'Не'],
        ['Майчинство', '32', '04.11.1922', '15.12.2012', 'Не'],
        ['Майчинство', '21', '04.09.2012', '14.05.2812', 'Да'],
        ['Майчинство', '999', '22.01.2222', '22.12.2222', 'Да']
      ]);
    });

    it('should cancel creating correctly', function () {
      newPersonStatusPage.personStatusType.set('Майчинство');
      newPersonStatusPage.documentNumber.set('999');
      newPersonStatusPage.documentDateValidFrom.set('22.01.2222');
      newPersonStatusPage.documentDateValidTo.set('22.12.2222');
      newPersonStatusPage.notes.set('test123');
      newPersonStatusPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      personStatusesPage = new SearchPage(ptor);

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

  });
}(protractor, describe, beforeEach, it, expect, require, xit));