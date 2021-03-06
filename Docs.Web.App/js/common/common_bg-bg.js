﻿/*global angular*/
(function (angular) {
  'use strict';
  angular.module('common').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      common: {
        login: {
          modalTitle: 'Вход',
          loginBtn: 'Влез',
          username: 'Потребителско име',
          password: 'Парола',
          invalidUsernameAndPassword: 'Невалидно потребителско име или парола'
        },
        navigation: {
          logout: 'Изход',
          changePassword: 'Смяна на паролата',
          docs: {
            title: 'Документи',
            search: 'Търсене',
            'new': 'Нов документ',
            news: 'Нови документи',
            corrsSearch: 'Кореспонденти',
            corrsNew: 'Нов кореспондент'
          },
          reports: {
            search: 'Справки'
          },
          admin: {
            title: 'Админ',
            users: 'Потребители',
            units: 'Организационна структура',
            noms: 'Номенклатури',
            nomenclatures: 'Общи номенклатури',
            docNomenclatures: 'Номенклатури деловодна'
          },
          help: {
            title: 'Помощ',
            userManual: 'Ръководство на потребителя'
          }
        },
        users: {
          search: {
            username: 'Потребителско име',
            name: 'Име',
            activity: 'Само активни',
            roles: 'Роли',
            active: 'Активен',
            noUsersFound: 'Няма намерени потребители',
            'new': 'Нов потребител',
            search: 'Търси'
          },
          edit: {
            username: 'Потребителско име:',
            name: 'Име:',
            usernameInvalid: 'потребителското име трябва да е поне 5 символа' +
              ' и да съдържа само букви, числа, подчертавки (_) и точки (.)',
            usernameExists: 'потребителското име е заето',
            email: 'Имейл',
            appointmentDate: 'Дата на назначаване',
            resignationdate: 'Дата на напускане',
            comment: 'Коментар:',
            entrance: 'Вход:',
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
        changePasswordModal: {
          title: 'Смяна на парола',
          oldPassword: 'Текуща парола',
          newPassword: 'Нова парола',
          confirmNewPassword: 'Повторете новата парола',
          noPasswordMatch: 'Паролите не съвпадат',
          passMustBeMin8symbols: 'Паролата трябва да бъде поне 8 символа',
          wrongPassword: 'Грешна парола',
          save: 'Запис',
          cancel: 'Отказ'
        },
        units: {
          title: 'Организационна единица',
          filter: 'Филтър',
          refresh: 'Презареди',
          includeInactive: 'Включи неактивните',
          editUnitTooltip: 'Промяна на организационна единица',
          activateUnitTooltip: 'Активиране на организационна единица',
          deactivateUnitTooltip: 'Деактивиране на организационна единица',
          deleteUnitTooltip: 'Премахване на организационна единица',
          addChildUnitTooltip: 'Добавяне на организационна единица',
          attachUnitToUserTooltip: 'Прикачане на организационна единица към потребител',
          detachUnitFromUserTooltip: 'Премахване на връзката между организационна единица и потребител',
          errors: {
            Unit_CannotBeDeleted_ExistingRelation: 'Oрганизационна единица не може да бъде изтрита, поради връзки с други обекти',
            Entity_CannotBeDeactivated: 'Oрганизационна единица не може да бъде деактивирана',
            Entity_CannotBeActivated: 'Oрганизационна единица не може да бъде активирана',
          },
          edit: {
            classification: 'Класификация',
            classificationPermissions: 'Права'
          },
          users: {
            title: 'Избери потребител',
            userName: 'Потребителско име',
            fullName: 'Име'
          }
        },
        docNomenclatures: {
          docType: 'Видове документи',
          docTypeGroup: 'Групи видове документи',
          removeIrregularityDeadline: 'Време за отстраняване на нередности',
          executionDeadline: 'Време за изпълнение',
          register: 'Регистър'
        }
      },
      commonLabels: {
        save: 'Запази',
        cancel: 'Откажи',
        type: 'Тип',
        name: 'Име',
        active: 'Активен',
        edit: 'Редактирай',
        add: 'Добави'
      },
      states: {
        'root.users': 'Потребители',
        'root.users.new': 'Нов потребител',
        'root.users.edit': 'Редакция',
        'root.units': 'Организационна структура',
        'root.docNomenclatures': 'Номенклатури деловодна',
        'root.docNomenclatures.docTypes': 'Видове документи',
        'root.docNomenclatures.docTypeGroups': 'Групи видове документи',
      }
    });
  }]);
}(angular));
