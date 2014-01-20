﻿/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Person status edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/personStatusPO'),
        SearchPage = require('../pageObjects/searchPersonStatusesPO'),
        editPersonStatusPage,
        personStatusesPage;

    beforeEach(function () {
      ptor.get('#/persons/1/statuses/4');
      editPersonStatusPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editPersonStatusPage.breadcrumb.getText()).toEqual('Редакция на състояние');
    });

    it('should display status data correctly', function () {
      expect(editPersonStatusPage.personStatusType.get()).toEqual('Негоден');
      expect(editPersonStatusPage.documentNumber.get()).toEqual('1');
      expect(editPersonStatusPage.documentDateValidFrom.get()).toEqual('07.10.1912');
      expect(editPersonStatusPage.documentDateValidTo.get()).toEqual('24.12.1912');
      expect(editPersonStatusPage.notes.get()).toEqual('note1');
    });

    it('should disable the save button when requried fields are not filled', function () {
      editPersonStatusPage.personStatusType.clear();
      expect(editPersonStatusPage.saveBtn.getAttribute('disabled')).toBe('true');
    });
    
    it('should fill and save status data correctly', function () {
      editPersonStatusPage.personStatusType.set('Майчинство');
      editPersonStatusPage.documentNumber.set('123');
      editPersonStatusPage.documentDateValidFrom.set('22.01.2012');
      editPersonStatusPage.documentDateValidTo.set('22.12.2014');
      editPersonStatusPage.notes.set('test');
      editPersonStatusPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      personStatusesPage = new SearchPage(ptor);

      expect(personStatusesPage.datatable.getColumns(
        'part.personStatusType.name', 'part.documentNumber', 'part.documentDateValidFrom',
        'part.documentDateValidTo', 'part.notes', 'part.isActive'))
      .toEqual([
        ['Майчинство', '123', '22.01.2012', '22.12.2014', 'test', 'Да'],
        ['Майчинство', '2', '04.04.1812', '04.05.1812', 'note2', 'Не'],
        ['Майчинство', '32', '04.11.1922', '15.12.2012', 'note3', 'Не'],
        ['Майчинство', '21', '04.09.2012', '14.05.2812', 'note4', 'Да']
      ]);
    });

    it('should cancel editing correctly', function () {
      editPersonStatusPage.personStatusType.set('Майчинство');
      editPersonStatusPage.documentNumber.set('123');
      editPersonStatusPage.documentDateValidFrom.set('22.01.2012');
      editPersonStatusPage.documentDateValidTo.set('22.12.2014');
      editPersonStatusPage.notes.set('test');
      editPersonStatusPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/statuses');

      personStatusesPage = new SearchPage(ptor);

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
    
  });
}(protractor, describe, beforeEach, it, expect, require));