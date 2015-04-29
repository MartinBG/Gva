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
            corrsNew: 'Нов кореспондент'
          },
          persons: {
            title: 'ЛАП',
            search: 'Физически лица',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление',
            exam: 'Нов теор.изпит АС',
            printableDocs: 'Документи за печат',
            stampedDocuments: 'Лицензи за получаване',
            examinationSystem: 'Изпитна система',
            'export': 'Експорт XML',
            references: 'Справки'
          },
          aircrafts: {
            title: 'ВС',
            search: 'Въздухоплавателни средства',
            registrations: 'Регистрации',
            appsSearch: 'Заявления',
            appsNew: 'Ново заявление',
            invalidActNumbers: 'Невалидни дел. номера'
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
          tabs: {
            personsCommon: 'Общи за ФЛ',
            licences: 'Лицензи',
            ratings: 'Квалификации',
            system: 'Системни номенклатури',
            persons: 'Номенклатури за ФЛ',
            aircrafts: 'Номенклатури за ВС	',
            organizations: 'Номенклатури за организации',
            common: 'Общи',
            '145mf': '145/MF',
            '147': '147',
            reports: 'Доклади',
            equipments: 'Номенклатури за съоръжения',
            airports: 'Номенклатури за летища'
          },
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
              nameAlt: 'Наименование на английски език',
              typeLimitation66: 'Тип ограничение',
              isActive: 'Активен'
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
              order: 'Пореден номер',
              isActive: 'Активен',
              limitations66Point: 'Точка на забележките',
              limType: 'Тип ограничение',
              caseTypes: 'Типове дела',
              caseType: 'Тип дело',
              isPersonsOnly: 'Само за лица',
              isIdDocument: 'За самоличност',
              categoryAlias: 'Категория документи',
              duration: 'Срок на валидност в дни',
              licenceTypeIds: 'Типове лицензи (за изпити)',
              ratingClassGroup: 'Група клас ВС',
              ratingClass: 'Клас ВС',
              authorizationGroup: 'Група Разрешение',
              aircraftType: 'Тип ВС',
              qualification: 'Квалификация (за изпити)',
              templateName: 'Темплейт',
              codeCA: 'Код на типа на лиценза видим при печат',
              country: 'Държава',
              auditPart: 'Раздел',
              auditPartSection: 'Раздел (част) в част 3 на доклад в подкрепа на',
              staffType: 'Тип персонал',
              makeRadio: 'Радио',
              makeAircraft: 'ВС',
              makePropeller: 'Витла',
              makeEngine: 'Двигатели'
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
        'root.nomenclatures.search.values': 'Списък стойности',
        'root.nomenclatures.search.values.edit': 'Редакция',
        'root.nomenclatures.search': 'Търсене',
        'root.nomenclatures.search.licences': 'Лицензи',
        'root.nomenclatures.search.ratings': 'Квалификации',
        'root.nomenclatures.search.persons': 'Номенклатури за ФЛ',
        'root.nomenclatures.search.aircrafts': 'Номенклатури за ВС	',
        'root.nomenclatures.search.airports': 'Номенклатури за Летища	',
        'root.nomenclatures.search.equipments': 'Номенклатури за Съоръжения	',
        'root.nomenclatures.search.orgCommon': 'Общи номенклатури за организации',
        'root.nomenclatures.search.org145mf': '145/MF',
        'root.nomenclatures.search.org147': '147', 
        'root.nomenclatures.search.orgReport': 'Доклади'
      }
    });
  }]);
}(angular));
