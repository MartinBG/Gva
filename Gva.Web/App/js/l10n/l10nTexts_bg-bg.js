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
      },
      persons: {
        personDataDirective: {
          title: 'Лични данни',
          lin: 'ЛИН',
          uin: 'ЕГН',
          dateOfBirth: 'Дата на раждане',
          sex: 'Пол',
          firstName: 'Име',
          firstNameAlt: 'Име (латиница)',
          middleName: 'Презиме',
          middleNameAlt: 'Презиме (латиница)',
          lastName: 'Фамилия',
          lastNameAlt: 'Фамилия (латиница)',
          placeOfBirth: 'Място на раждане',
          country: 'Гражданство',
          email: 'E-mail',
          fax: 'Факс',
          companyPhone: 'Служебен телефон',
          phones: 'Телефони'
        },
        personAddressDirective: {
          title: 'Адрес',
          addressType: 'Вид',
          settlement: 'Населено място',
          address: 'Адрес',
          addressAlt: 'Адрес (латиница)',
          valid: 'Валиден',
          postalCode: 'Пощенски код',
          phone: 'Телефон'
        },
        personDocumentIdDirective: {
          title: 'Документ за самоличност',
          personDocumentIdTypeId: 'Тип документ',
          valid: 'Валиден',
          documentNumber: 'Номер на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валиден до',
          documentPublisher: 'Издаден от'
        },
        personStatusDirective: {
          title: 'Състояние',
          personStatusType: 'Причина',
          documentNumber: 'Номер на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки'
        },
        newPerson: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        viewPerson: {
          name: 'Име',
          uin: 'ЕГН',
          lin: 'ЛИН',
          age: 'Възраст',
          company: 'Фирма',
          employmentCategory: 'Длъжност',
          edit: 'Редакция'
        },
        newAddress: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAddress: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        addressSearch: {
          type: 'Вид',
          settlement: 'Населено място',
          address: 'Адрес',
          postalCode: 'Пощенски код',
          phone: 'Телефон',
          valid: 'Актуален',
          edit: 'Редакция',
          remove: 'Изтрий'
        },
        newStatus: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editStatus: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        statusSearch: {
          newState: 'Ново Състояние',
          personStatusType: 'Причина',
          documentNumber: 'Номер на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки',
          isActive: 'Активен',
          edit: 'Редакция',
          remove: 'Изтрий'
        }
      }
    });
  }]);
}(angular));
