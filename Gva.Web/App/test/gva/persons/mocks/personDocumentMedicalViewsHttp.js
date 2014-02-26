/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentMedicalViews',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var meds = _(person.personDocumentMedicals)
          .forEach(function (m) {
            var testimonial = m.part.documentNumberPrefix + '-' +
              m.part.documentNumber + '-' +
              person.personData.part.lin + '-' +
              m.part.documentNumberSuffix;
            m.part.testimonial = testimonial;

            var limitations = '';
            for (var i = 0; i < m.part.limitationsTypes.length; i++) {
              limitations += m.part.limitationsTypes[i].name + ', ';
            }
            limitations = limitations.substring(0, limitations.length - 2);
            m.part.limitations = limitations;
          }).value();

          return [200, meds];

        });
  });
}(angular, _));