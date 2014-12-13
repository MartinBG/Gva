/*global angular*/
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
            corrsNew: 'Нов кореспондент',
            printableDocs: 'Документи за печат'
          },
          persons: {
            title: 'ЛАП',
            search: 'Физически лица',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление',
            exam: 'Нов теор.изпит АС',
            stampedDocuments: 'Документи за печат'
          },
          aircrafts: {
            title: 'ВС',
            search: 'Въздухоплавателни средства',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление'
          },
          airports: {
            title: 'ЛЛП',
            search: 'Летища и лет. площадки',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление'
          },
          equipments: {
            title: 'СУВД',
            search: 'Системи за УВД',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление'
          },
          approvedOrgs: {
            title: 'ОО',
            search: 'Одобрени организации',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление'
          },
          airportOperators: {
            title: 'ЛО',
            search: 'Летищни оператори'
          },
          groundSvcOperators: {
            title: 'ОНО',
            search: 'Оператори наземно обслужване'
          },
          educationOrgs: {
            title: 'АУЦ',
            search: 'Авиационни учебни центрове'
          },
          airCarriers: {
            title: 'ВП',
            search: 'Въздушни превозвачи'
          },
          airOperators: {
            title: 'АО',
            search: 'Авиационни оператори'
          },
          airNavSvcProviders: {
            title: 'ДАО',
            search: 'Доставчици на АНО'
          },
          admin: {
            title: 'Админ',
            users: 'Потребители',
            nomenclatures: 'Номенклатури'
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
            unit: 'Служител',
            inputUnit: 'изберете служител',
            unitExists: 'служителят е вече избран',
            roles: 'Роли:',
            active: 'Активен:',
            save: 'Запис',
            cancel: 'Отказ'
          }
        },
        nomenclatures: {
          noms: {
            search: {
              name: 'Наименование'
            },
            edit: {
              title: 'Вид номенклатура',
              save: 'Запис',
              cancel: 'Отказ',
              name: 'Наименование',
              alias: 'Псевдоним'
            }
          },
          nomValues: {
            search: {
              newNomValue: 'Нова стойност',
              code: 'Код',
              name: 'Наименование',
              nameAlt: 'Наименование на английски език'
            },
            edit: {
              title: 'Номенклатура',
              save: 'Запис',
              cancel: 'Отказ',
              remove: 'Изтриване',
              nomType: 'Вид на номенклатурата',
              code: 'Код',
              name: 'Наименование',
              altName: 'Алт. наименование',
              alias: 'Псевдоним',
              parentNomType: 'Вид на родителската номенклатура',
              parentNomValue: 'Идентификатор на родителската номенклатура',
              order: 'Пореден номер',
              isActive: 'Активен'
            }
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
        }
      },
      states: {
        'root.users': 'Потребители',
        'root.users.new': 'Нов потребител',
        'root.users.edit': 'Редакция',
        'root.nomenclatures': 'Общи номенклатури',
        'root.nomenclatures.new': 'Нов вид номенклатура',
        'root.nomenclatures.edit': 'Редакция',
        'root.nomenclatures.values': 'Списък стойности',
        'root.nomenclatures.values.edit': 'Редакция'
      }
    });
  }]);
}(angular));
