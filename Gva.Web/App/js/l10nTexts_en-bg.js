(function (angular) {
  'use strict';
  angular.module('l10nTexts_en-bg', ['l10n']).config(['l10nProvider', function(l10n){
    l10n.add('bg', {
      navbar: {
        exit: 'Изход',
        changePass: 'Смяна на паролата'
      },
      index: {
        unableToShowPage: 'Страницата не може да бъде показана,' +
          ' ако сте спрели изпълнението на JavaScript',
        pleaseWait: 'Моля изчакайте...',
        licence: 'Лицензиране на авиационен персонал,' +
          ' въздухоплавателни средства и летателна годност'
      },
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
    });
  }]);
}(angular));
