/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person flying experience new page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../../pageObjects/flyingExperiences/newFlyingExpPO'),
       SearchPage = require('../../pageObjects/flyingExperiences/searchFlyingExpPO'),
       newFlyingExpPage,
       searchFlyingExpPage;

    beforeEach(function () {
      ptor.get('#/persons/1/flyingExperiences/new');
      newFlyingExpPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newFlyingExpPage.breadcrumb.getText())
        .toEqual('Нов летателен / практически опит');
    });

    it('should create new flying experience correctly', function () {
      newFlyingExpPage.chooseStaffType('Членове на екипажа').then(function () {
        newFlyingExpPage.month.set('Ноември');
        newFlyingExpPage.year.set('2012');
        newFlyingExpPage.aircraft.set('LZ-002 Вива --56 V-56');
        newFlyingExpPage.experienceRole.set('Под наблюдение на инструктор');
        newFlyingExpPage.ratingClass.set('Свръхлеки самолети');
        newFlyingExpPage.ratingType.set('Tу 154');
        newFlyingExpPage.experienceMeasure.set('Брой полети');
        newFlyingExpPage.dayLandings.set('23');
        newFlyingExpPage.nightLandings.set('46');
        newFlyingExpPage.totalDocHours.set('56');
        newFlyingExpPage.totalDocMinutes.set('45');
        newFlyingExpPage.save();

        expect(ptor.getCurrentUrl()).toEqual(
          'http://localhost:52560/#/persons/1/flyingExperiences'
        );
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
              '2014',
              'Януари',
              'LZ-001 A-11',
              'BAe 146',
              'Много леки самолети',
              'Отработени часове',
              '5',
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
            ],
            [
              '2012',
              'Ноември',
              'LZ-002 Вива --56 V-56',
              'Tу 154',
              'Свръхлеки самолети',
              'Брой полети',
              '23',
              '46'
            ]
          ]);
      });
    }, 20000);

    it('should go to search view at clicking on cancel button', function () {
      newFlyingExpPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/flyingExperiences');
    });
  });

}(protractor, describe, beforeEach, it, expect));