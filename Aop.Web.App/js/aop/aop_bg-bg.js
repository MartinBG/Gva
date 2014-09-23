/*global angular*/
(function (angular) {
  'use strict';
  angular.module('aop').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      apps: {
        search: {
          expert: 'Експерт',
          employer: 'Възложител',
          email: 'Имейл',
          'new': 'Ново заявление',
          search: 'Търси',
          docIstage: 'Документ за I етап',
          docIIstage: 'Документ за II етап'
        },
        edit: {
          edit: 'Редакция',
          remove: 'Изтриване',
          confirmDelete: 'Сигурни ли сте, че искате да изтриете данните?',
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      checklist: {
        createDate: 'Дата на създаване',
        author: 'Автор',
        currentChecklist: 'Версии на документа',
        compareTo: 'Сравни с версия'
      },
      states: {
        'root.apps': 'Заявления',
        'root.apps.new': 'Ново заявление',
        'root.apps.edit': 'Редакция'
      }
    });
  }]);
}(angular));
