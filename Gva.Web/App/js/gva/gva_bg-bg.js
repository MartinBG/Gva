/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      persons: {
        search: {
          names: 'Име',
          lin: 'ЛИН',
          uin: 'ЕГН',
          licences: 'Лицензи',
          ratings: 'Квалификации',
          organization: 'Организация',
          age: 'Възраст',
          yes: 'Да',
          no: 'Не',
          'new': 'Ново лице',
          search: 'Търси',
          view: 'Преглед'
        },
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
          documentPublisher: 'Издаден от',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          notes: 'Бележки'
        },
        personScannedDocumentDirective: {
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл'
        },
        personApplicationDirective: {
          title: 'Документът е приложен към заявления:',
          name: 'Име на заявление',
          number: 'Номер на заявление'
        },
        personStatusDirective: {
          title: 'Състояние',
          personStatusType: 'Причина',
          documentNumber: 'Номер на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки'
        },
        personFlyingExperienceDirective: {
          title: 'Летателен/практически опит',
          staffType: 'Тип персонал',
          month: 'За месец',
          year: 'Година',
          organization: 'Организация',
          aircraft: 'Рег.знак на ВС',
          ratingType: 'Тип ВС',
          ratingClass: 'Клас',
          licenceType: 'Кв.ниво',
          authorization: 'Разрешение',
          locationIndicator: 'Местоположение',
          sector: 'Сектор/раб.място',
          experienceRole: 'Роля',
          experienceMeasure: 'Вид опит',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          documentDate: 'Дата на документа',
          dayDuration: 'Нальот(дневен)',
          nightDuration: 'Нальот(нощен)',
          IFR: 'IFR',
          VFR: 'VFR',
          dayLandings: 'Кацания(ден)',
          nightLandings: 'Кацания(нощ)',
          total: 'Общо количество (с натрупване)',
          totalDoc: 'Общо количество (по документа)',
          totalLastMonths: 'Общ нальот за посл. 12 месеца',
          notUnique: 'Данните се дублират с вече съществуващ запис'
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
          newAddress: 'Нов aдрес',
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
        },
        newDocumentId: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentId: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        documentIdSearch: {
          docTypeId: 'Документ',
          documentNumber: 'Номер на документа',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валиден до',
          valid: 'Валиден',
          bookPageNumber: 'Номер на страница в деловодна книга.',
          pageCount: 'Брой стр.',
          file: 'Файл',
          edit: 'Редакция',
          remove: 'Изтрий',
          newDocumentId: 'Нов документ'
        },
        flyingExperienceSearch: {
          staffType: 'Тип персонал',
          month: 'За месец',
          year: 'Година',
          organization: 'Организация',
          aircraft: 'Рег.знак на ВС',
          ratingType: 'Тип ВС',
          ratingClass: 'Клас',
          licenceType: 'Кв.ниво',
          authorization: 'Разрешение',
          locationIndicator: 'Местоположение',
          sector: 'Сектор/раб.място',
          experienceRole: 'Роля',
          experienceMeasure: 'Вид опит',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          documentDate: 'Дата на документа',
          edit: 'Редакция',
          remove: 'Изтрий',
          newFlyingExperience: 'Нов летателен/практически опит'
        },
        flyingExperienceNew: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        flyingExperienceEdit: {
          save: 'Запис',
          cancel: 'Отказ'
        }
      }
    });
  }]);
}(angular));
