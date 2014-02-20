/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person flying experience edit page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../../pageObjects/flyingExperiences/editFlyingExpPO'),
       SearchPage = require('../../pageObjects/flyingExperiences/searchFlyingExpPO'),
       editFlyingExpPage,
       searchFlyingExpPage;

    beforeEach(function () {
      ptor.get('#/persons/1/flyingExperiences/22');
      editFlyingExpPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editFlyingExpPage.breadcrumb.getText())
        .toEqual('Редакция на летателен / практически опит');
    });

    it('should display correct filled out data', function () {
      expect(editFlyingExpPage.staffType.get()).toEqual('Членове на екипажа');
      expect(editFlyingExpPage.month.get()).toEqual('Януари');
      expect(editFlyingExpPage.year.get()).toEqual('2014');
      expect(editFlyingExpPage.organization.get()).toEqual('AAK Progres');
      expect(editFlyingExpPage.aircraft.get()).toEqual('LZ-001 A-11');
      expect(editFlyingExpPage.ratingType.get()).toEqual('BAe 146');
      expect(editFlyingExpPage.ratingClass.get()).toEqual('Много леки самолети');
      expect(editFlyingExpPage.authorization.get()).toEqual('Летателен инструктор на самолет');
      expect(editFlyingExpPage.experienceRole.get()).toEqual('Самостоятелен');
      expect(editFlyingExpPage.experienceMeasure.get()).toEqual('Отработени часове');
      expect(editFlyingExpPage.dayIfrHours.get()).toEqual('23');
      expect(editFlyingExpPage.dayIfrMinutes.get()).toEqual('33');
      expect(editFlyingExpPage.nightIfrHours.get()).toEqual('25');
      expect(editFlyingExpPage.nightIfrMinutes.get()).toEqual('35');
      expect(editFlyingExpPage.dayVfrHours.get()).toEqual('24');
      expect(editFlyingExpPage.dayVfrMinutes.get()).toEqual('34');
      expect(editFlyingExpPage.nightVfrHours.get()).toEqual('26');
      expect(editFlyingExpPage.nightVfrMinutes.get()).toEqual('36');
      expect(editFlyingExpPage.dayLandings.get()).toEqual('5');
      expect(editFlyingExpPage.nightLandings.get()).toEqual('15');
      expect(editFlyingExpPage.totalDocHours.get()).toEqual('26');
      expect(editFlyingExpPage.totalDocMinutes.get()).toEqual('36');
      expect(editFlyingExpPage.totalLastMonthsHours.get()).toEqual('26');
      expect(editFlyingExpPage.totalLastMonthsMinutes.get()).toEqual('36');
      expect(editFlyingExpPage.totalHours.get()).toEqual('26');
      expect(editFlyingExpPage.totalMinutes.get()).toEqual('36');
    });

    it('should change flying experience data correctly', function () {
      editFlyingExpPage.month.set('Декември');
      editFlyingExpPage.year.set('2013');
      editFlyingExpPage.aircraft.set('LZ-002 Вива --56 V-56');
      editFlyingExpPage.ratingType.set('Let L-410 Turbolet');
      editFlyingExpPage.ratingClass.set('Свръхлеки самолети');
      editFlyingExpPage.experienceMeasure.set('Летателни часове');
      editFlyingExpPage.dayLandings.set('15');
      editFlyingExpPage.save();

      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/flyingExperiences');
      searchFlyingExpPage = new SearchPage(ptor);

      expect(searchFlyingExpPage.datatable.getColumns(
        'part_period_year',
        'part_period_month',
        'part_aircraft_name',
        'part_ratingType_name',
        'part_ratingClass_name',
        'part_experienceMeasure_name',
        'part_dayLandings',
        'part_nightLandings'
        )).toEqual([
          [
            '2013',
            'Декември',
            'LZ-002 Вива --56 V-56',
            'Let L-410 Turbolet',
            'Свръхлеки самолети',
            'Летателни часове',
            '15',
            '15'
          ],
          [
            '2014',
            'Февруари',
            'LZ-001 A-11',
            'BAe 146',
            'Много леки самолети',
            'Отработени часове',
            '5',
            '15'
          ]
        ]);
    });
  });

}(protractor, describe, beforeEach, it, expect));