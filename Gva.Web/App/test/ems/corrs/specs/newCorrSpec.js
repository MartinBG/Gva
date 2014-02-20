/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('New correspondent page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/newCorrPO'),
       SearchPage = require('../pageObjects/searchCorrPO'),
       newCorrPage,
       searchCorrPage;

    beforeEach(function () {
      ptor.get('#/corrs/new');
      newCorrPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newCorrPage.breadcrumb.getText())
        .toEqual('Нов кореспондент');
    });

    it('should go to corr search on cancel click', function () {
      newCorrPage.cancel();

      searchCorrPage = new SearchPage(ptor);

      expect(searchCorrPage.breadcrumb.getText())
        .toEqual('Кореспонденти');
    });

    it('should create new correspondent correctly', function () {
      newCorrPage.correspondentGroup.set('Заявители');
      newCorrPage.correspondentType.set('Български гражданин');
      newCorrPage.email.set('ivan.ivanov@mail.bg');
      newCorrPage.bgCitizenFirstName.set('Иван');
      newCorrPage.bgCitizenLastName.set('Иванов');

      newCorrPage.save();

      searchCorrPage = new SearchPage(ptor);

      expect(searchCorrPage.breadcrumb.getText())
        .toEqual('Кореспонденти');

      expect(searchCorrPage.datatable.getColumns(
          'displayName',
          'email',
          'correspondentType_name'
          )).toEqual([
        ['ДЕЛТА КОИН 1324567890', 'delta@coin.com', 'Български гражданин'],
        ['АЛИ БАБА 4040404040', 'delta@coin.com', 'Чужденец'],
        ['Иван Иванов', 'ivan.ivanov@mail.bg', 'Български гражданин']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));
