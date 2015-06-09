/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      scaffolding: {
        scFiles: {
          manyFiles: '{{filesCount}} прикачени файла.',
          noFiles: 'Няма прикачени файлове.',
          noFile: 'Няма прикачен файл.',
          failAlert: 'Възникна грешка. Успешно качените файлове са записани. Опитайте отново.',
          modal: {
            titleMultiple: 'Прикачени файлове',
            titleSingle: 'Прикачен файл',
            attachSingle: 'Избери файл',
            attachMultiple: 'Избери файлове',
            accept: 'Прикачи',
            cancel: 'Отказ',
            noFilesAttached: 'Няма прикачени файлове',
            noFileAttached: 'Няма прикачен файл'
          }
        },
        scDatatable: {
          firstPage: 'Първа страница',
          lastPage: 'Последна страница',
          nextPage: 'Следваща',
          previousPage: 'Предишна  ',
          info: 'Намерени общo {{total}} резултата (от {{start}} до {{end}})',
          datatableInfo: 'Показани резултати от ',
          to: ' до ',
          all: ' от общо ',
          noDataAvailable: 'Няма намерени резултати',
          displayRecords: ' на страница',
          search: 'Търси',
          filtered: ' (филтрирани от {{max}} записа)',
          deleteColumns: 'Колони'
        },
        scTreetable: {
          noDataAvailable: 'Няма намерени резултати'
        },
        scMessage: {
          okButton: 'OK',
          cancelButton: 'Отказ'
        },
        scSearchButton: {
          clear: 'Изчисти',
          filtersSetName: 'Име на групата филтри'
        },
        scSearch: {
          confirmDelete: 'Сигурни ли сте, че искате да изтриете данните?'
        }
      }
    });
  }]);
}(angular));
