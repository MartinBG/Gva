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
          title: 'Образование',
          documentNumber: '№ на документ',
          completionDate: 'Дата на завършване',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          speciality: 'Специалност',
          graduation: 'Степен на образование',
          school: 'Учебно заведение',
          notes: 'Бележки'
        },
        personDocumentIdDirective: {
          title: 'Документ за самоличност',
          personDocumentIdTypeId: 'Тип документ',
          valid: 'Валиден',
          documentNumber: '№ на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валиден до',
          documentPublisher: 'Издаден от',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          notes: 'Бележки'
        },
        personScannedDocumentDirective: {
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл'
        },
        personApplicationDirective: {
          title: 'Документът е приложен към заявления:',
          name: 'Име на заявление',
          number: '№ на заявление',
          view: 'Преглед'
        },
        personStatusDirective: {
          title: 'Състояние',
          personStatusType: 'Причина',
          documentNumber: '№ на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки'
        },
        personMedicalDirective: {
          testimonial: 'Свидетелство',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          limitations: 'Ограничения към свидетелство за медицинска годност',
          medClassType: 'Клас',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
          title: 'Свидетелство за медицинска годност'
        },
        personEmploymentDirective: {
          title: 'Месторабота',
          hiredate: 'Дата на назначаване',
          valid: 'Валиден',
          organization: 'Организация',
          employmentCategory: 'Категория длъжност',
          country: 'Страна',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          notes: 'Бележки'
        },
        personCheckDirective: {
          title: 'Проверка',
          staffType: 'Вид персонал',
          documentNumber: '№ на документа',
          documentPersonNumber: '№ в списъка',
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
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          notes: 'Бележки',
          sector: 'Сектор/работно място',
          locationIndicator: 'Индикатор на местоположение',
          ratingType: 'Тип ВС'
        },
        personDocumentTrainingDirective: {
          title: 'Обучение',
          staffType: 'Тип персонал',
          documentNumber: '№ на документ',
          documentPersonNumber: '№ в списъка (групов документ)',
          documentDateValidFrom: 'Дата на завършване',
          documentDateValidTo: 'Срок на валидност',
          documentPublisher: 'Издател',
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
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.'
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
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          documentDate: 'Дата',
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
        ratingEditionDirective : {
          title:'Вписване/Потвърждение',
          documentDateValidFrom: 'Дата на вписване',
          documentDateValidTo: 'Валидно до',
          inspector: 'Инспектор',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          limitations: 'Ограничения',
          subclasses: 'Подкв.класове'
        },
        ratingDirective: {
          title: 'Клас',
          staffType: 'Тип персонал',
          ratingType: 'Тип ВС',
          personRatingLevel: 'Степен',
          sector: 'Сектор/работно място',
          locationIndicator: 'Индикатор на местоположение',
          ratingClass: 'Клас ВС',
          authorization: 'Разрешение',
          aircraftTypeGroup: 'Тип/Група ВС',
          ratingCategory: 'Категория',
          personRatingModel: 'Модел'
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
          postalCode: 'П.К.',
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
          documentNumber: '№ на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки',
          isActive: 'Валидно'
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
          documentNumber: '№ на документа',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валиден до',
          publisher: 'Издаден от',
          valid: 'Валиден',
          bookPageNumber: '№ стр. в делов. книга',
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
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
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
          organization: 'Фирма',
          country: 'Страна',
          valid: 'Валидна',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
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
          documentNumber: '№ на документа',
          completionDate: 'Дата на завършване',
          school: 'Учебно заведение',
          graduation: 'Степен на образование',
          speciality: 'Специалност',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentEducation: 'Ново образование'
        },
        checkSearch: {
          newCheck: 'Нова проверка',
          documentNumber: '№ на документа',
          ratingClass: 'Клас',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          personCheckDocumentType: 'Тип документ',
          personCheckDocumentRole: 'Роля на документ',
          ratingType: 'Тип ВС (раб. място)',
          valid: 'Валидност',
          ratingValue: 'Оценка',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
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
          documentNumber: '№ на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          ratingType: 'Тип ВС (раб. място)',
          ratingClass: 'Клас',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля на документ',
          valid: 'Валидно',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
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
          month: 'Месец',
          year: 'Година',
          organization: 'Организация',
          aircraft: 'ВС',
          ratingType: 'Тип ВС (раб. място)',
          ratingClass: 'Клас',
          licenceType: 'Вид правоспособност',
          authorization: 'Разрешение',
          locationIndicator: 'Местоположение',
          sector: 'Сектор/работно място',
          experienceRole: 'Роля',
          experienceMeasure: 'Мерна единица',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          documentDate: 'Дата',
          dayIFR: 'Дневен IFR',
          nightIFR: 'Нощен IFR',
          dayVFR: 'Дневен VFR',
          nightVFR: 'Нощен VFR',
          dayLandings: 'Дневни кац.',
          nightLandings: 'Нощни кац.',
          totalDoc: 'Общо количество',
          totalLastMonths: 'Общо за 12 мес. назад',
          total: 'Общо с натрупване',
          newFlyingExperience: 'Нов летателен/практически опит',
          file: 'Файл'
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
          bookPageNumber: '№ на страница',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл'
        },
        ratingSearch: {
          newRating : 'Нов клас',
          ratingTypeOrRatingLevel: 'Тип ВС/Степен',
          classOrCategory: 'Клас/Подклас (Категория)',
          authorizationAndLimitations: 'Разрешение (ограничения)',
          firstEditionValidFrom: 'Първоначално издаване',
          documentDateValidFrom: 'Издаден',
          documentDateValidTo: 'Валиден до',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          personRatingModel: 'Модел'
        },
        ratingEditionsSearch: {
          newEdition : 'Ново вписване/Потвърждаване',
          ratingTypeOrRatingLevel: 'Тип ВС/Степен',
          classOrCategory: 'Клас/Подклас (Категория)',
          authorizationAndLimitations: 'Разрешение (ограничения)',
          firstEditionValidFrom: 'Първоначално издаване',
          documentDateValidFrom: 'Дата на издаване',
          documentDateValidTo: 'Валиден до',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          personRatingModel: 'Модел',
          inspector: 'Инспектор'
        },
        ratingEditionNew: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        ratingEditionEdit: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        ratingNew: {
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      applications: {
        newform: {
          person: 'Заявител',
          newPerson: 'Нов заявител',
          choosePerson: 'Избери заявител'
        },
        choosePerson: {
          choose: 'Избери',
          cancel: 'Отказ'
        },
        link: {
          chooseDoc: 'Избор на документ',
          cancel: 'Отказ',
          clear: 'Изчисти',
          document: 'Документ',
          docNumber: 'Рег.№',
          docStatus: 'Статус',
          docName: 'Име'
        },
        search: {
          fromDate: 'От дата',
          toDate: 'До дата',
          search: 'Търси',
          regDate: 'Дата',
          regUri: 'Рег.№',
          subject: 'Относно',
          status: 'Статус',
          lin: 'ЛИН',
          applicant: 'Заявител'
        }
      },
      errorTexts: {
        required: 'Задължително поле'
      }
    });
  }]);
}(angular));
