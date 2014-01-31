﻿/*global angular*/
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
          search: 'Търси'
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
        personDocumentEducationDirective: {
          title: 'Образнование',
          documentNumber: 'Номер на документ',
          completionDate: 'Дата на завършване',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          speciality: 'Специалност',
          graduation: 'Степен на образование',
          school: 'Учебно заведение',
          notes: 'Бележки'
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
        personMedicalDirective: {
          documentNumberPrefix: 'Префикс',
          documentNumber: 'Номер',
          documentNumberSuffix: 'Суфикс',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          limitations: 'Ограничения към свидетелство за медицинска годност',
          medClassType: 'Клас',
          notes: 'Бележки',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници',
          title: 'Свидетелство за медицинска годност'
        },
        personEmploymentDirective: {
          title: 'Месторабота',
          hiredate: 'Дата на назначаване',
          valid: 'Валиден',
          organization: 'Организация',
          employmentCategory: 'Категория длъжност',
          country: 'Страна',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          notes: 'Бележки'
        },
        personCheckDirective: {
          title: 'Проверка',
          staffType: 'Вид персонал',
          documentNumber: 'Номер на документа',
          documentPersonNumber: 'Номер в списъка',
          personCheckDocumentType: 'Тип документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          ratingClass: 'Клас ВС',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          personCheckRatingValue: 'Оценка',
          personCheckDocumentRole: 'Роля на документ',
          aircraftTypeGroup: 'Тип/Група ВС',
          valid: 'Валиден',
          bookPageNumber: '№ на стр. в деловодна книга',
          pageCount: 'Брой страници на документа',
          notes: 'Бележки',
          sector: 'Сектор/работно място',
          locationIndicator: 'Индикатор на местоположение',
          ratingType: 'Тип ВС'
        },
        personDocumentTrainingDirective: {
          title: 'Обучение',
          staffType: 'Тип персонал',
          documentNumber: 'Номер на документ',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издаден от',
          ratingType: 'Тип ВС',
          aircraftTypeGroup: 'Тип/Група ВС',
          ratingClass: 'Клас ВС',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          locationIndicator: 'Индикатор за местоположение',
          sector: 'Сектор/работно място',
          engLangLevel: 'Ниво на език',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля на документ',
          valid: 'Валиден',
          notes: 'Бележки',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа'
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
          valid: 'Актуален'
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
          isActive: 'Активен'
        },
        newDocumentId: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentId: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        newDocumentEducation: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentEducation: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        documentIdSearch: {
          docTypeId: 'Документ',
          documentNumber: 'Номер на документа',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валиден до',
          publisher: 'Издаден от',
          valid: 'Валиден',
          bookPageNumber: 'Номер на страница в деловодна книга.',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentId: 'Нов документ'
        },
        medicalSearch: {
          testimonial: 'Свидетелство',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          medClass: 'Клас',
          limitations: 'Ограничения',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          file: 'Файл',
          newMedical: 'Ново медицинско'
        },
        newMedical: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editMedical: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        employmentSearch: {
          newEmployment: 'Нова месторабота',
          hiredate: 'Дата на назначаване',
          employmentCategory: 'Категория длъжност',
          organization: 'Организация',
          country: 'Страна',
          valid: 'Валидност',
          notes: 'Бележки',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа.',
          file: 'Файл'
        },
        newEmployment: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editEmployment: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        documentEducationSearch: {
          documentNumber: 'Номер на документа',
          completionDate: 'Дата на завършване',
          school: 'Учебно заведение',
          graduation: 'Степен на образование',
          speciality: 'Специалност',
          bookPageNumber: 'Номер на стр. в деловодна книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentEducation: 'Ново образнование'
        },
        checkSearch: {
          newCheck: 'Нова проверка',
          documentNumber: '№ на документа',
          ratingClass: 'Клас ВС',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          personCheckDocumentType: 'Тип документ',
          personCheckDocumentRole: 'Роля на документ',
          ratingType: 'Тип ВС',
          valid: 'Валидност',
          notes: 'Бележки',
          bookPageNumber: '№ на стр. в дел. книга',
          pageCount: 'Бр. стр.',
          file: 'Файл'
        },
        editCheck: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        newCheck: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        documentTrainingSearch: {
          staffType: 'Тип персонал',
          documentNumber: 'Номер на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издаден от',
          ratingType: 'Тип ВС',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля на документ',
          valid: 'Валиден',
          notes: 'Бележки',
          bookPageNumber: 'Номер на страница в деловодна книга',
          pageCount: 'Брой страници на документа',
          file: 'Файл',
          newDocumentTraining: 'Нов документ'
        },
        newDocumentTraining: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentTraining: {
          save: 'Запис',
          cancel: 'Отказ'
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
          newFlyingExperience: 'Нов летателен/практически опит'
        },
        flyingExperienceNew: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        flyingExperienceEdit: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        inventorySearch: {
          bookPageNumber: 'Номер на страница',
          document: 'Документ',
          type: 'Вид',
          docNumber: 'Номер на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. страници',
          file: 'Файл'
        }
      }
    });
  }]);
}(angular));
