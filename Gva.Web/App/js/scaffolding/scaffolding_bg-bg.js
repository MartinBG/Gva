/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      scaffolding: {
        scFiles: {
          manyFiles: '{{filesCount}} прикачени файла.',
          noFiles: 'Няма прикачени файлове.',
          modal: {
            title: 'Прикачени файлове',
            accept: 'Запис',
            cancel: 'Отказ',
            noFilesAttached: 'Няма прикачени файлове'
          }
        },
        scDatatable: {
          firstPage: 'Първа страница',
          lastPage: 'Последна страница',
          nextPage: 'Следваща',
          previousPage: 'Предишна  ',
          info: 'Намерени общo _TOTAL_ резултата (от _START_ до _END_)',
          datatableInfo: 'Показани резултати от ',
          to: ' до ',
          all: ' от общо ',
          noDataAvailable: 'Няма намерени резултати',
          displayRecords: '_MENU_ на страница',
          search: 'Търси',
          filtered: ' (филтрирани от _MAX_ записа)',
          deleteColumns: 'Колони'
        }
      }
    });
  }]);
}(angular));
