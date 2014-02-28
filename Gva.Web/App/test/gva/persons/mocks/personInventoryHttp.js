/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/inventory',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var documentEducations = _.map(person.personDocumentEducations, function (element) {
            return {
              documentType: 'education',
              partIndex: element.partIndex,
              name: 'Образование',
              bookPageNumber: element.part.bookPageNumber,
              type: element.part.graduation.name,
              number: element.part.documentNumber,
              date: element.part.completionDate,
              publisher: element.part.school.name,
              valid: null,
              fromDate: null,
              toDate: null,
              pageCount: element.part.pageCount,
              file: element.file ? element.file[0] : null,
              createdBy: null,
              creationDate: null,
              editedBy: null,
              editedDate: null
            };
          }),
              documentIds = _.map(person.personDocumentIds, function (element) {
            return {
              documentType: 'documentId',
              partIndex: element.partIndex,
              name: 'Документ за самоличност',
              bookPageNumber: element.part.bookPageNumber,
              type: element.part.personDocumentIdType.name,
              number: element.part.documentNumber,
              date: element.part.documentDateValidFrom,
              publisher: element.part.documentPublisher,
              valid: element.part.valid.name,
              fromDate: element.part.documentDateValidFrom,
              toDate: element.part.documentDateValidTo,
              pageCount: element.part.pageCount,
              file: element.file ? element.file[0] : null,
              createdBy: null,
              creationDate: null,
              editedBy: null,
              editedDate: null
            };
          }),
              documentTrainings = _.map(person.personDocumentTrainings, function (element) {
            return {
              documentType: 'training',
              partIndex: element.partIndex,
              name: element.part.personOtherDocumentRole.name,
              bookPageNumber: element.part.bookPageNumber,
              type: element.part.personOtherDocumentType.name,
              number: element.part.documentNumber,
              date: element.part.documentDateValidFrom,
              publisher: element.part.documentPublisher,
              valid: element.part.valid.name,
              fromDate: element.part.documentDateValidFrom,
              toDate: element.part.documentDateValidTo,
              pageCount: element.part.pageCount,
              file: element.file ? element.file[0] : null,
              createdBy: null,
              creationDate: null,
              editedBy: null,
              editedDate: null
            };
          }),
              documentMedicals = _.map(person.personDocumentMedicals, function (element) {
            return {
              documentType: 'medical',
              partIndex: element.partIndex,
              name: 'Медицинско свидетелство',
              bookPageNumber: element.part.bookPageNumber,
              type: null,
              number: element.part.documentNumberPrefix + '-' +
                element.part.documentNumber + '-' +
                person.personData.part.lin + '-' +
                element.part.documentNumberSuffix,
              date: element.part.documentDateValidFrom,
              publisher: element.part.documentPublisher,
              valid: null,
              fromDate: element.part.documentDateValidFrom,
              toDate: element.part.documentDateValidTo,
              pageCount: element.part.pageCount,
              file: null,
              createdBy: null,
              creationDate: null,
              editedBy: null,
              editedDate: null
            };
          }),
             documentChecks = _.map(person.personDocumentChecks, function (element) {
            return {
              documentType: 'check',
              partIndex: element.partIndex,
              name: element.part.personCheckDocumentRole === undefined ?
                null :
                element.part.personCheckDocumentRole.name,
              bookPageNumber: element.part.bookPageNumber,
              type: null,
              number: element.part.documentNumber,
              date: element.part.documentDateValidFrom,
              publisher: element.part.documentPublisher,
              valid: element.part.valid.name,
              fromDate: element.part.documentDateValidFrom,
              toDate: element.part.documentDateValidTo,
              pageCount: element.part.pageCount,
              file: null,
              createdBy: null,
              creationDate: null,
              editedBy: null,
              editedDate: null
            };
          });

          return [200, _.union(
                documentEducations,
                documentIds,
                documentTrainings,
                documentMedicals,
                documentChecks)];
        });
  });
}(angular, _));