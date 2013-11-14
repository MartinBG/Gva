/*global angular*/
(function (angular) {
  'use strict';
  angular.module('l10nTexts_bg-bg', ['l10n']).config(['l10nProvider', function(l10n){
    l10n.add('bg-bg', {
      navbar: {
        exit: 'Изход',
        changePass: 'Смяна на паролата'
      },
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
        }
      },
      users: {
        search: {
          username: 'Потребителско име',
          name: 'Име',
          activity: 'Активност',
          onlyActive: 'Само активни',
          onlyUnactive: 'Само неактивни',
          roles: 'Роли',
          active: 'Активен',
          noUsersFound: 'Няма намерени потребители',
          yes: 'Да',
          no: 'Не',
          'new': 'Нов потребител',
          search: 'Търси',
          edit: 'Редакция'
        },
        edit: {
          username: 'Потребителско име:',
          name: 'Име:',
          usernameInvalid: 'потребителското име трябва да е поне 5 символа' +
            ' и да съдържа само букви, числа, подчертавки (_) и точки (.)',
          usernameExists: 'потребителското име е заето',
          comment: 'Коментар:',
          entrance:'Вход:',
          withPassAndUsername: ' с потребителско име / парола',
          withCertificate: ' със сертификат',
          password: 'Парола:',
          passMustBeMin8symbols: 'паролата трябва да бъде поне 8 символа',
          repeatPass: 'Повтори парола:',
          doNotMatch: 'паролите не съвпадат',
          certificate: 'Сертификат:',
          inputCertificate: 'въведете сертификат',
          roles: 'Роли:',
          active: 'Активен:',
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      datatableDirective: {
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
    });
  }]);
}(angular));
