/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person flying experience search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/flyingExperiences/searchFlyingExpPO'),
        personFlyingExpPage;

    beforeEach(function () {
      ptor.get('#/persons/1/flyingExperiences');
      personFlyingExpPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personFlyingExpPage.breadcrumb.getText()).toEqual('Летателен / практически опит');
    });

    it('should display data correctly', function () {
      expect(personFlyingExpPage.datatable.getColumns(
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
        ]
      ]);
    });

    it('should delete a flying experience', function () {
      personFlyingExpPage.firstDeleteBtn.click();
      personFlyingExpPage = new Page(ptor);
      expect(personFlyingExpPage.datatable.getColumns(
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

    it('should go to edit page', function () {
      personFlyingExpPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl()).toEqual(
          'http://localhost:52560/#/persons/1/flyingExperiences/22'
        );
      });
    });
  });

}(protractor, describe, beforeEach, it, expect));