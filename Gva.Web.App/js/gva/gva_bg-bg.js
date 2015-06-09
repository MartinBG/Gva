/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      common: {
        title: 'Лицензиране на авиационен персонал, въздухоплавателни средства и летателна годност',
        messages: {
          confirmDelete: 'Сигурни ли сте, че искате да изтриете данните?'
        },
        choosePublisherModal: {
          title: 'Избор на издател',
          lin: 'ЛИН',
          publisherType: 'Тип',
          name: 'Наименование',
          search: 'Търси',
          select: 'Избор',
          code: 'Код',
          cancel: 'Отказ',
          inspector: 'Инспектор',
          examiner: 'Проверяващ',
          school: 'Учебен център',
          organization: 'Авио-организация',
          caa: 'Въздушна администрация',
          other: 'Други'
        },
        wordTemplateDataGenerator: {
          cancel: 'Отказ',
          save: 'Запис',
          title: 'Избор на генератор на данни за темплейт',
          templateName: 'Наименование на темплейт',
          dataGenerator: 'Генератор'
        },
        linkAppModal: {
          title: 'Свързване със заявление',
          choosePortalApp: 'Избор на заявление от портала',
          chooseGvaApp: 'Избор на тип заявление в ГВА',
          chooseCaseType: 'Избор на тип дело',
          forward: 'Напред',
          app: 'Заявление',
          chooseLot: 'Избор на партида',
          back: 'Назад',
          chooseBtn: 'Избор',
          newLot: 'Нова партида',
          cancel: 'Отказ',
          saveLot: 'Запис',
          personDataForm: 'Лични данни',
          setAircraftWizzardData: 'Продължи',
          select: 'Избери',
          name: 'Наименование',
          code: 'Код',
          search: 'Търси',
          caseType: 'Тип дело'
        },
        newPersonModal: {
          newApplicant: 'Нов заявител',
          newPerson: 'Ново физическо лице',
          save: 'Запис',
          cancel: 'Отказ',
          personDataForm: 'Лични данни'
        },
        choosePersonModal: {
          title: 'Избор на заявител',
          personTitle: 'Избор на физическо лице',
          cancel: 'Отказ',
          select: 'Избери',
          lin: 'ЛИН',
          uin: 'ЕГН',
          names: 'Име',
          licences: 'Лицензи',
          ratings: 'Квалификации',
          organization: 'Организация',
          search: 'Търси',
          age: 'Възраст'
        },
        editDisparityModal: {
          title: 'Редакция на несъответствие',
          save:'Запис',
          cancel: 'Отказ',
          refNumber: 'Реф. №',
          disparityLevel: 'Ниво',
          rectifyAction: 'Внесени коригиращи действия',
          closureDocument: '№ на документ за закриване',
          removalDate: 'Дата за отстраняване',
          closureDate: 'Дата на закриване',
          description: 'Описание на несъответствие'
        },
        viewApplicationModal: {
          title: 'Преглед на заяление',
          toApp: 'Към заявление',
          cancel: 'Отказ'
        },
        applicationAlertInfoDirective: {
          viewApplication: 'Обратно към заявление: '
        },
        selectPersonDirective: {
          person: 'Заявител',
          newPerson: 'Нов заявител'
        },
        docApplicationDirective: {
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          notes: 'Бележки',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса',
          taxNumber: '№ на такса',
          caseType: 'Тип дело'
        },
        scannedDocumentDirective: {
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл',
          bookPageNumber: '№ стр.',
          pageCount: 'Бр. стр.',
          caseType: 'Тип дело',
          applications: 'Заявления',
          note: 'Бележка'
        },
        inspectionDetailsDirective: {
          title: 'Главни обобщени констатации',
          subject: 'Тема',
          auditResult: 'Констатация',
          disparity: 'Несъответствия',
          code: 'Код',
          insertInspectionDetails: 'Въведи списъка за обобщени констатации',
          disparitiesTitle: 'Несъответствия',
          index: '№',
          refNumber: 'Реф. №',
          description: 'Описание на несъответствие',
          disparityLevel: 'Ниво',
          removalDate: 'Дата за отстраняване',
          closureDate: 'Дата на закриване',
          rectifyAction: 'Внесени коригиращи действия',
          closureDocument: '№ на документ за закриване',
          noAvailableDisparities: 'Няма добавени несъответствия',
          addAuditor: 'Добави одитор',
          auditor: 'Одитор',
          auditors: 'Одитори',
          noAvailableAuditors: 'Няма добавени одитори'
        },
        inspectionDataDirective: {
          documentNumber: '№ на документ',
          auditPart: 'Част',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          auditState: 'Състояние',
          notification: 'Предв. уведом.',
          subject: 'Предмет на одит',
          caseType: 'Тип дело',
          inspectionPlace: 'Адрес на одитирания обект',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          controlCard: 'Контролна карта'
        }
      },
      airports: {
        tabs: {
          docs: 'Документи',
          certs: 'Удостоверения',
          owners: 'Свързани лица',
          others: 'Други',
          opers: 'Експлоатационна годност',
          applications: 'Заявления',
          inspections: 'Инспекции',
          inventory: 'Опис'
        },
        search: {
          airportType: 'Тип',
          name: 'Наименование',
          place: 'Местоположение',
          icao: 'ICAO код',
          runway: 'Полоса',
          course: 'Курс',
          excess: 'Превишение ',
          concrete: 'Полоса-бетон',
          'new': 'Ново летище',
          search: 'Търси'
        },
        chooseDocumentsModal: {
          title: 'Избор на документи',
          add: 'Добави',
          cancel: 'Отказ',
          search: 'Търси',
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
        airportDataDirective: {
          airportType: 'Тип',
          name: 'Наименование',
          nameAlt: 'Наименование (англ.)',
          place: 'Местоположение',
          icao: 'ICAO код',
          runway: 'Полоса',
          course: 'Курс',
          excess: 'Превишение ',
          concrete: 'Полоса-бетон',
          latitude: 'Ширина',
          longitude: 'Дължина',
          frequencies: 'Радиочестоти',
          frequency: 'Честота',
          radioNavigationAids: 'Радионавигационни средства',
          aid: 'Средство',
          parameters: 'Параметри',
          noAvailableFrequencies: 'Няма налични радиочестоти',
          noAvailableAids: 'Няма налични радионавигационни средства',
          coordinates: 'Координати на контролна точка'
        },
        viewAirport: {
          airportType: 'Тип',
          name: 'Наименование',
          nameAlt: 'Наименование (англ.)',
          icao: 'ICAO код',
          runway: 'Полоса',
          course: 'Курс',
          edit: 'Редакция'
        },
        newAirport: {
          title: 'Ново съоръжение',
          airportDataForm: 'Данни за летище',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAirportData: {
          title: 'Преглед на данни за летище',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        airportOtherDirective: {
          documentNumber: 'Док No',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          otherDocumentType: 'Тип документ',
          airportOtherDocumentRole: 'Роля',
          valid: 'Действителен'
        },
        newOther: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Нов документ'
        },
        editOther: {
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция',
          deleteOther: 'Изтриване',
          title: 'Преглед на документ'
        },
        otherSearch: {
          documentNumber: 'Документ №',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          otherDocumentType: 'Тип документ',
          otherDocumentRole: 'Роля',
          valid: 'Валидно',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ',
          file: 'Файл',
          notes: 'Бележки'
        },
        inspectionSearch: {
          newInspection: 'Нова инспекция',
          documentNumber: '№ на документ',
          auditState: 'Състояние',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          subject: 'Предмет на одит',
          notification: 'Оператора предварително уведомен',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          inspectionPlace: 'Адрес на одитирания обект',
          application: 'Преписка (Заявление)'
        },
        newInspection: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Нова инспекция за летище'
        },
        editInspection: {
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция',
          deleteInspection: 'Изтриване',
          title: 'Преглед на инспекция за летище'
        },
        airportOwnerDirective: {
          airportRelation: 'Тип отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: '№ на документ',
          documentDate: 'Дата на документ',
          fromDate: 'В сила от',
          toDate: 'Дата на прекратяване на отношенията',
          reasonTerminate: 'Причина за прекратяване',
          notes: 'Бележки'
        },
        newOwner: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново свързано лице'
        },
        editOwner: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteOwner: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на свързано лице'
        },
        ownerSearch: {
          newOwner: 'Ново свързано лице',
          airportRelation: 'Отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: 'Документ №',
          documentDate: 'Дата на документ',
          fromDate: 'От дата',
          toDate: 'До дата',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          notes: 'Бележки'
        },
        airportOperDirective: {
          issueNumber: 'Номер на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          inspector: 'Проверил',
          valid: 'Валиден',
          includedDocuments: 'Приложени документи',
          approvalDate: 'Дата на одобрение',
          docInspector: 'Инспектор',
          linkedDocument: 'Документ',
          ext: 'Продължение',
          extDate: 'Дата на издаване',
          extValidToDate: 'Дата на изтичане',
          extInspector: 'Проверил',
          revoke: 'Отнемане',
          revokeDate: 'Дата на отнемане',
          revokeInspector: 'Инспектор',
          revokeCause: 'Причина за отнемане',
          chooseDocuments: 'Избери документи',
          noAvailableDocuments: 'Няма налични приложени документи'
        },
        newOper: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново удостоверение'
        },
        editOper: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteOper: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на удостоверение'
        },
        operSearch: {
          newOper: 'Ново удостоверение',
          issueDate: 'Дата на издаване',
          issueNumber: 'Удостоверение №',
          validToDate: 'Срок на валидност',
          organization: 'Организация',
          inspector: 'Проверил',
          valid: 'Валиден'
        },
        airportDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса',
          file: 'Файл'
        },
        newAirportDocApplication: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново заявление'
        },
        editAirportDocApplication: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteApplication: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на заявление'
        },
        inventorySearch: {
          bookPageNumber: '№ на стр.',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл',
          notIndexed: 'Документи извън описа'
        }
      },
      equipments: {
        tabs: {
          docs: 'Документи',
          certs: 'Удостоверения',
          owners: 'Свързани лица',
          others: 'Други',
          opers: 'Експлоатационна годност',
          applications: 'Заявления',
          inspections: 'Инспекции',
          inventory: 'Опис'
        },
        search: {
          equipmentType: 'Тип',
          name: 'Наименование',
          equipmentProducer: 'Производител',
          manPlace: 'Място на производство',
          manDate: 'Дата на производство',
          place: 'Местоположение',
          operationalDate: 'Дата на въвеждане в експлоатация',
          'new': 'Ново съоръжение',
          search: 'Търси'
        },
        chooseDocumentsModal: {
          title: 'Избор на документи',
          add: 'Добави',
          cancel: 'Отказ',
          search: 'Търси',
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
        equipmentDataDirective: {
          equipmentType: 'Тип',
          name: 'Наименование',
          equipmentProducer: 'Производител',
          manPlace: 'Място на производство',
          manDate: 'Дата на производство',
          place: 'Местоположение',
          operationalDate: 'Дата на въвеждане в експлоатация',
          note: 'Допълнително описание',
          otherData: 'Допълнителни данни',
          coordinates: 'Координати',
          elevation: 'Кота на терена',
          call: 'Позивна',
          frequencies: 'Работни честоти',
          behavior: 'Режим на работа',
          power: 'Изходяща мощност',
          range: 'Радиус на действие'
        },
        viewEquipment: {
          equipmentType: 'Тип',
          name: 'Наименование',
          equipmentProducer: 'Производител',
          manPlace: 'Място на производство',
          manDate: 'Дата на производство',
          place: 'Местоположение',
          edit: 'Редакция'
        },
        newEquipment: {
          title: 'Ново съоръжение',
          equipmentDataForm: 'Данни за съоръжение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editEquipmentData: {
          title: 'Преглед на данни за съоръжение',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        equipmentOtherDirective: {
          documentNumber: 'Док No',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          otherDocumentType: 'Тип документ',
          equipmentOtherDocumentRole: 'Роля',
          valid: 'Действителен'
        },
        newOther: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Нов документ'
        },
        editOther: {
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция',
          deleteOther: 'Изтриване',
          title: 'Преглед на документ'
        },
        otherSearch: {
          newDocument: 'Нов документ',
          documentNumber: 'Документ №',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          otherDocumentType: 'Тип документ',
          otherDocumentRole: 'Роля',
          valid: 'Валидно',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ',
          file: 'Файл'
        },
        inspectionSearch: {
          newInspection: 'Нова инспекция',
          documentNumber: '№ на документ',
          auditState: 'Състояние',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          subject: 'Предмет на одит',
          notification: 'Оператора предварително уведомен',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          inspectionPlace: 'Адрес на одитирания обект',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          application: 'Преписка (Заявление)'
        },
        newInspection: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Нова инспекция за съоръжения'
        },
        editInspection: {
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция',
          deleteInspection: 'Изтриване',
          title: 'Преглед на инспекция за съоръжения'
        },
        equipmentOwnerDirective: {
          title: 'Свързано лице',
          equipmentRelation: 'Тип отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: '№ на документ',
          documentDate: 'Дата на документ',
          fromDate: 'В сила от',
          toDate: 'Дата на прекратяване на отношенията',
          reasonTerminate: 'Причина за прекратяване',
          notes: 'Бележки'
        },
        newOwner: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново свързано лице'
        },
        editOwner: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteOwner: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на свързано лице'
        },
        ownerSearch: {
          newOwner: 'Ново свързано лице',
          equipmentRelation: 'Отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: 'Документ №',
          documentDate: 'Дата на документ',
          fromDate: 'От дата',
          toDate: 'До дата',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл'
        },
        equipmentOperDirective: {
          title: 'Удостоверение за експлоатационна годност',
          issueNumber: 'Номер на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          inspector: 'Проверил',
          valid: 'Валиден',
          includedDocuments: 'Приложени документи',
          approvalDate: 'Дата на одобрение',
          docInspector: 'Инспектор',
          linkedDocument: 'Документ',
          noAvailableDocs: 'Няма налични документи',
          ext: 'Продължение',
          extDate: 'Дата на издаване',
          extValidToDate: 'Дата на изтичане',
          extInspector: 'Проверил',
          revoke: 'Отнемане',
          revokeDate: 'Дата на отнемане',
          revokeInspector: 'Инспектор',
          revokeCause: 'Причина за отнемане',
          noAvailableDocuments: 'Няма налични приложени документи',
          chooseDocuments: 'Избери документи'
        },
        newOper: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново удостоверение'
        },
        editOper: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteOper: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на удостоверение'
        },
        operSearch: {
          newOper: 'Ново удостоверение',
          issueDate: 'Дата на издаване',
          issueNumber: 'Удостоверение №',
          validToDate: 'Срок на валидност',
          organization: 'Организация',
          audit: 'Инспекция',
          inspector: 'Проверил',
          valid: 'Валиден'
        },
        equipmentDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса',
          file: 'Файл'
        },
        newEquipmentDocApplication: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново заявление'
        },
        editEquipmentDocApplication: {
          save: 'Запис',
          cancel: 'Отказ',
          deleteApplication: 'Изтриване',
          edit: 'Редакция',
          title: 'Преглед на заявление'
        },
        inventorySearch: {
          bookPageNumber: '№ на стр.',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл',
          notIndexed: 'Документи извън описа'
        }
      },
      sModeCodes: {
        search: {
          'new': 'Нов Код',
          search: 'Търси',
          codeHex: 'Код в шестнайсетична бр.с-ма',
          type: 'Вид код',
          note: 'Бележки',
          codeDecimal: 'Код в десетична бр.с-ма',
          codeBinary: 'Код в двоична бр.с-ма'
        },
        newSModeCode: {
          title: 'Нов S-Mode Code',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editSModeCode: {
          title: 'Редакция на S-Mode Code',
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция'
        },
        sModeCodeDataDirective: {
          codeHex: 'Код в шестнайсетична бр.с-ма',
          codeType: 'Вид код',
          note: 'Бележки',
          connectToAircraft: 'Избери ВС',
          viewAircraft: 'Покажи ВС',
          valid: 'Валиден'
        }
      },
      aircrafts: {
        tabs: {
          reg: 'Регистрация',
          currentReg: 'Последна регистрация',
          regs: 'Регистрации',
          airworthinesses: 'Летателни годности',
          smods: 'S-code',
          radios: 'Разрешителни за радиостанция',
          noises: 'Удостоверения за шум',
          docs: 'Документи',
          debts: 'Тежести',
          occurrences: 'Инциденти',
          inspections: 'Инспекции',
          owners: 'Свързани лица',
          others: 'Други',
          opers: 'Експлоатационна годност',
          applications: 'Заявления',
          inventory: 'Опис'
        },
        search: {
          manSN: 'Сериен номер',
          model: 'Модел',
          modelAlt: 'Модел',
          outputDate: 'Дата на производство',
          icao: 'ICAO код',
          airCategory: 'AIR категория',
          aircraftProducer: 'Производител',
          engine: 'Двигател',
          propeller: 'Витло',
          ModifOrWingColor: 'Модификация/Цвят на крило',
          engineAlt: 'Двигател',
          propellerAlt: 'Витло',
          ModifOrWingColorAlt: 'Модификация/Цвят на крило',
          'new': 'Ново ВС',
          search: 'Търси',
          mark: 'Рег. знак',
          actNumber: 'Дел. №',
          certNumber: 'Рег. №'
        },
        registrations: {
          actNumber: 'Деловоден №',
          certNumber: 'Регистрационен №',
          search: 'Търси',
          regMark: 'Регистрационен. знак',
          register: 'Регистър'
        },
        manageRadioEntryModal: {
          title: 'Ново оборудване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        aircraftScannedDocumentDirective: {
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          caseType: 'Тип дело',
          applications: 'Заявления'
        },
        aircraftDataDirective: {
          manSN: 'Сериен номер',
          model: 'Модел',
          modelAlt: 'Модел (английски)',
          outputDate: 'Дата на произв.',
          icao: 'ICAO код',
          aircraftCategory: 'Тип ВС',
          aircraftProducer: 'Производител',
          engine: 'Двигател',
          engineAlt: 'Двигател (английски)',
          propeller: 'Витло',
          propellerAlt: 'Витло (английски)',
          ModifOrWingColor: 'Модификация/Цвят на крило',
          ModifOrWingColorAlt: 'Модификация/Цвят на крило (английски)',
          docRoom: 'Документи в стая',
          cofAType: 'CofA Type',
          airCategory: 'AIR Category',
          easaCategory: 'EASA Категория',
          euRegType: 'EASA Reg',
          maxMassL: 'Макс. маса при кацане/Полезен товар (при VLA)',
          maxMassT: 'Макс. маса при излитане (MTOM)',
          seats: 'Брой места',
          issueNumber: '№',
          tcdsn: 'TCDSN',
          tcds: 'TCDS',
          chapter: 'Chapter',
          issueDate: 'Дата на издаване',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане',
          noiseData: 'Удостоверение за шум',
          aircraftType: 'Type of Aircraft'
        },
        newWizzardDirective: {
          aircraftProducer: 'Производител',
          airCategory: 'AIR категория',
          aircraftModel: 'Модел ВС'
        },
        newAircraft: {
          title: 'Ново ВС',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newWizzard: {
          title: 'Ново ВС',
          save: 'Запис',
          forward: 'Напред',
          back: 'Назад',
          cancel: 'Отказ'
        },
        editAircraft: {
          title: 'Преглед на данни за ВС',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        viewAircraft: {
          aircraftProducer: 'Производител',
          airCategory: 'AIR категория',
          icao: 'ICAO',
          model: 'Модел ВС',
          modelAlt: 'Модел ВС (английски)',
          manSN: 'MSN (сериен номер)',
          cofAType: 'CofA Type',
          edit: 'Редакция',
          actNumber: 'Деловоден номер',
          certNumber: 'Регистрационен номер',
          mark: 'Регистрационен знак'
        },
        invalidActNumbersSearch: {
          actNumber: 'Дел. номер',
          register: 'Регистър',
          reason: 'Причина',
          devalidateActNumber: 'Добави към невалидните дел. номера',
          notExistingActNumber: 'Не съществува такъв дел. номер'
        },
        regFMSearch: {
          isActive: 'Активна',
          isCurrent: 'Последна',
          certNumber: 'Рег. номер',
          actNumber: 'Дел. номер',
          certDate: 'Дата на регистрация',
          register: 'Регистър',
          regMark: 'Регистрационен знак',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          inspector: 'Заверил',
          owner: 'Собственик',
          operator: 'Оператор',
          newReg: 'Нова регистрация'
        },
        newReg: {
          newReg: 'Нова регистрация',
          rereg: 'Пререгистрация',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newRegWizzard: {
          title: 'Нова регистрация',
          forward: 'Напред',
          back: 'Назад',
          cancel: 'Отказ',
          register: 'Регистър',
          certNumber: 'Рег. номер',
          actNumber: 'Дел. номер',
          skipNumber: 'Пропусни',
          regMark: 'Регистрационен знак',
          confirmRegMark: 'Желаният рег. знак е свободен.',
          regMarkInUse: 'Желаният рег. знак е запазен.'
        },
        editReg: {
          title: 'Преглед на регистрация',
          dereged: 'Преглед на дерегистрация',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteReg: 'Изтриване',
          dereg: 'Дерегистрация',
          rereg: 'Пререгистрация',
          removeDereg: 'Премахни данните за дерегистрация'
        },
        deregReg: {
          title: 'Дерегистриране',
          save: 'Запис',
          cancel: 'Отказ',
          deregData: 'Данни за дерегистрация'
        },
        regView: {
          first: 'Първа рег.',
          last: 'Последна рег.',
          prev: 'Предходна рег.',
          next: 'Следваща рег.',
          firstReg: 'Първа рег № ',
          lastReg: 'Посл. рег. № ',
          regFrom: ' от ',
          debts: 'Тежести',
          newReg: 'Нова регистрация'
        },
        smodSearch: {
          theirNumber: 'Тяхно писмо №',
          theirDate: 'Тяхна дата',
          caaNumber: 'ГВА писмо №',
          caaDate: 'ГВА дата',
          codeHex: 'Код в шестнайсетична бр.с-ма',
          codeDecimal: 'Код в десетична бр.с-ма',
          codeBinary: 'Код в двоична бр.с-ма'
        },
        editSmod: {
          title: 'Преглед на mode S код',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          search: 'Търси',
          codeHex: 'Код в шестнайсетична бр.с-ма',
          note: 'Бележки',
          codeDecimal: 'Код в десетична бр.с-ма',
          codeBinary: 'Код в двоична бр.с-ма',
          sModeCode: 'S-mode код'
        },
        airworthinessSearch: {
          newAirworthiness: 'Нова годност',
          airworthinessCertificateType: 'Тип сертификат',
          issueDate: 'Дата на издаване',
          validFromDate: 'Валидно от',
          validToDate: 'Валидно до',
          inspector: 'Заверил'
        },
        newAirworthiness: {
          airworthinessCertificateType: 'Тип',
          titleAw: 'Нова летателна годност',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAirworthiness: {
          titleAw: 'Преглед на удостоверение за летателна годност',
          editAw: 'Редакция',
          deleteAw: 'Изтрий',
          saveAw: 'Запис',
          cancelAw: 'Отказ',
          status: 'Статус',
          issueDate: 'Издадено на',
          from: 'Валидно от',
          to: 'Валидно до',
          inspector: 'Инспектор'
        },
        noiseSearch: {
          tcdsn: 'TCDSN',
          issueDate: 'Дата на издаване',
          issueNumber: '№',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане',
          newNoise: 'Ново удостоверение'
        },
        newNoise: {
          title: 'Ново удостоверение за съответствие с нормите за авиационен шум',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editNoise: {
          title: 'Преглед на удостоверение за съответствие с нормите за авиационен шум',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteNoise: 'Изтрий'
        },
        radioSearch: {
          newRadio: 'Ново разрешително',
          issueDate: 'Дата на издаване',
          inspector: 'Заверил',
          aslNumber: 'ASL №'
        },
        newRadio: {
          title: 'Ново разрешително за използване на радиостанция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRadio: {
          title: 'Преглед на разрешително за използване на радиостанция',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteRadio: 'Изтрий'
        },
        debtSearchFM: {
          regDate: 'Дата',
          aircraftDebtType: 'Тежест',
          documentNumber: 'Вх.док ГВА',
          documentDate: 'Дата на док',
          isActive: 'Активна',
          aircraftApplicant: 'Заявител',
          inspector: 'Заверил',
          newDebt: 'Нова тежест',
          notes: 'Забележка',
          file: 'Файл'
        },
        newDebtFM: {
          title: 'Нова тежест',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDebtFM: {
          title: 'Преглед на тежест',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteDebt: 'Изтрий',
          close: 'Погасяване'
        },
        regViewDirective: {
          lastRegTitle: 'Последна регистрация',
          regTitle: 'Регистрация',
          currentCert: 'Текущ запис',
          lastCert: 'Последен запис',
          firstCert: 'Първи запис',
          certNumber: 'Рег. №',
          actNumber: 'Дел. №',
          register: 'Регистър',
          regMark: 'Рег. знак',
          certDate: 'Дата на рег.',
          status: 'Статус',
          owner: 'Собственик',
          oper: 'Оператор',
          catAW: 'Категория на опериране',
          limitations: 'Ограничения',
          aircraftTypeCertificateType: 'Типов сертификат',
          dereg: 'Дерегистрирай',
          rereg: 'Пререгистрирай',
          view: 'Прегледай',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'От дата',
          category: 'Категория',
          leasingDocNumber: 'Одобрение на лизинг',
          leasingDocDate: 'Дата',
          leasingLessor: 'Лизингодател',
          leasingAgreement: 'Договор за лизинг и анекси към него',
          leasingEndDate: 'Срок'
        },
        airworthinessViewDirective: {
          title: 'Летателна годност',
          titleNoAw: '(от предходна регистрация)',
          aircraftCertificateType: 'Тип',
          regNumber: '№',
          refNumber: 'Реф.№',
          issueDate: 'Издадено на',
          validToDate: 'Валидно до',
          validFromDate: 'Валидно от',
          inspector: 'Заверил',
          'new': 'Нова ЛГ',
          status: 'Статус'
        },
        smodDirective: {
          theirNumber: 'Тяхно писмо №',
          theirDate: 'Тяхна дата',
          caaNumber: 'ГВА писмо №',
          caaDate: 'ГВА дата',
          applicant: 'Заявител',
          getScode: 'Генерирай S-код',
          person: 'ФЛ',
          organization: 'ЮЛ'
        },
        airworthinessDirective: {
          airworthinessCertificateType: 'Тип',
          registration: 'Регистрация',
          documentNumber: 'Документ №',
          issueDate: 'Дата на издаване',
          validFromDate: 'Валиден от',
          validToDate: 'Валиден до',
          issueDateSpecial: 'Дата изд. на екс. огр.'
        },
        airworthinessForm15Directive: {
          airworthinessCertificateType: 'Тип',
          registration: 'Регистрация',
          validFromDate: 'Валиден от',
          validToDate: 'Валиден до',
          organization: 'Организация'
        },
        noiseDirective: {
          issueNumber: '№',
          standart: 'Стандарт',
          standartAlt: 'Стандарт (англ.)',
          issueDate: 'Дата на издаване',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане',
          modifications: 'Модификации',
          modificationsAlt: 'Модификации (англ.)',
          notes: 'Забележки',
          tcdsn: 'TCDSN',
          chapter: 'Chapter',
          additionalModification: 'Допълнителна модификация',
          additionalModificationAlt: 'Допълнителна модификация (англ.)'
        },
        radioDirective: {
          model: 'Модел',
          ASLNumber: 'ASL №',
          issueDate: 'Дата на издаване',
          regMark: 'Рег. знак',
          equipment: 'Апаратура',
          count: 'Брой',
          power: 'Мощност',
          classOfEmission: 'Клас на излъчване',
          bandwidth: 'Честотни ленти или честоти',
          addNewEntry: 'Добави апаратура',
          otherType: 'Вид',
          noEntries: 'Няма данни',
          organization: 'ЮЛ',
          person: 'ФЛ',
          checkedBy: 'Заверил',
          inspector: 'Инспектор ГВА',
          other: 'Друг'
        },
        radioEntryDirective: {
          model: 'Модел',
          equipment: 'Апаратура',
          count: 'Брой',
          power: 'Мощност',
          classOfEmission: 'Клас на излъчване',
          bandwidth: 'Честотни ленти или честоти',
          otherType: 'Вид'
        },
        regFMDirective: {
          certNumber: 'Рег. номер',
          actNumber: 'Дел. номер',
          certDate: 'Дата на регистрация',
          regMark: 'Рег. знак',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          incomingDocDesc: 'Други документи или причини',
          inspector: 'Инспектор',
          owner: 'Собственик',
          operator: 'Оператор',
          catAW: 'Категория на опериране',
          limitation: 'Ограничения',
          leasingDocNumber: 'Одобрение на лизинг',
          leasingDocDate: 'Дата',
          leasingLessor: 'Лизингодател',
          leasingAgreement: 'Договор за лизинг и анекси към него',
          leasingEndDate: 'Срок',
          status: 'Статус',
          EASA25Number: 'EASA Form 25',
          EASA25Date: 'Дата',
          EASA15Date: 'EASA Form 15',
          cofRDate: 'CofR_New',
          noiseDate: 'Noise New',
          noiseNumber: 'Noise New №',
          paragraph: 'В съответствие с параграф',
          paragraphAlt: 'В съответствие с параграф (англ.)',
          removal: 'Отписване',
          removalDate: 'Дата на отписване',
          removalReason: 'Основание за отписване',
          removalText: 'Основание за отписване - описание',
          removalDocumentNumber: 'Номер на документ за отписване',
          removalCountry: 'Държава',
          notes: 'Забележки/Изключения',
          notesAlt: 'Забележки/Изключения (англ.)',
          text: 'Текст',
          textAlt: 'Текст (англ.)',
          person: 'ФЛ',
          organization: 'ЮЛ',
          'export': 'Експортно удостоверение за ЛГ',
          aircraftIsNew: 'Ново ВС',
          aircraftIsUsed: 'Използвано ВС'
        },
        debtDirectiveFM: {
          regDate: 'Дата',
          regTime: 'Час',
          aircraftDebtType: 'Тежест',
          documentNumber: 'Вх.док ГВА',
          documentDate: 'Дата на док',
          aircraftApplicant: 'Заявител',
          notes: 'Забележка',
          theirDate: 'Тяхна дата',
          theirNumber: 'Техен №',
          inspector: 'Заверил',
          isActive: 'Активна'
        },
        debtCloseDirective: {
          title: 'Погасяване',
          date: 'Дата на погасяване',
          caaDoc: 'Наш документ №',
          caaDate: 'Наша дата',
          theirDate: 'Тяхна дата',
          theirNumber: 'Техен №',
          inspector: 'Заверил',
          notes: 'Забележка'
        },
        aircraftOtherDirective: {
          title: 'Друг документ',
          documentNumber: 'Док No',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          otherDocumentType: 'Тип документ',
          otherDocumentRole: 'Роля',
          valid: 'Действителен'
        },
        newOther: {
          title: 'Нов документ',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOther: {
          title: 'Преглед на документ',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteDoc: 'Изтрий'
        },
        otherSearch: {
          newDocument: 'Нов документ',
          documentNumber: 'Документ №',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          otherDocumentType: 'Тип документ',
          otherDocumentRole: 'Роля',
          valid: 'Валидно',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newOther: 'Нов документ',
          notes: 'Бележки'
        },
        aircraftOwnerDirective: {
          aircraftRelation: 'Тип отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: '№ на документ',
          documentDate: 'Дата на документ',
          fromDate: 'В сила от',
          toDate: 'Дата на прекратяване на отношенията',
          reasonTerminate: 'Причина за прекратяване',
          notes: 'Бележки'
        },
        newOwner: {
          title: 'Ново свързано лице',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOwner: {
          title: 'Преглед на свързано лице',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteOwner: 'Изтрий'
        },
        ownerSearch: {
          newOwner: 'Ново свързано лице',
          aircraftRelation: 'Отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: 'Документ №',
          documentDate: 'Дата на документ',
          fromDate: 'От дата',
          toDate: 'До дата',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newOther: 'Нов документ',
          notes: 'Бележки'
        },
        inspectionSearch: {
          newInspection: 'Нова инспекция',
          documentNumber: '№ на документ',
          auditState: 'Състояние',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          subject: 'Предмет на одит',
          notification: 'Оператора предварително уведомен',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          inspectionPlace: 'Адрес на одитирания обект',
          application: 'Преписка (Заявление)'
        },
        newInspection: {
          title: 'Нова инспекция за въздухоплавателни средства',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editInspection: {
          title: 'Преглед на инспекция за въздухоплавателни средства',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          inspectionPeriod: 'Период на надзора',
          inspectionFrom: 'от',
          inspectionTo: 'до',
          deleteInspection: 'Изтриване'
        },
        occurrenceSearch: {
          localTime: 'Час на инцидента',
          newOccurrence: 'Нов инцидент',
          localDate: 'Дата на инцидента',
          aircraftOccurrenceClass: 'Клас',
          country: 'Държава',
          area: 'Място на инцидента',
          occurrenceNotes: 'Бележки по инцидента',
          description: 'Описание',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл'
        },
        newOccurrence: {
          title: 'Нов инцидент',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOccurrence: {
          title: 'Преглед на инцидент',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteOcc: 'Изтрий'
        },
        occurrenceDirective: {
          localDate: 'Дата на инцидента',
          localTime: 'Час на инцидента',
          aircraftOccurrenceClass: 'Клас',
          country: 'Държава',
          area: 'Място на инцидента',
          occurrenceNotes: 'Бележки по инцидента',
          description: 'Описание'
        },
        aircraftDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        newAircraftDocApplication: {
          title: 'Ново заявление',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAircraftDocApplication: {
          title: 'Преглед на заявление',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteApplication: 'Изтрий'
        },
        inventorySearch: {
          bookPageNumber: '№ на стр.',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл',
          notIndexed: 'Документи извън описа'
        },
        inspectorDirective: {
          checkedBy: 'Заверил',
          inspector: 'Инспектор ГВА',
          examiner: 'Персонал за преглед',
          other: 'Друг'
        }
      },
      persons: {
        tabs: {
          licences: 'Лицензи',
          qualifications: 'Квалификации',
          ratings: 'Класове',
          flyingExperiences: 'Летателен / практически опит',
          documentTrainings: 'Обучение',
          documentLangCerts: 'Свид. за език',
          checks: 'Проверки',
          medicals: 'Медицински',
          personData: 'Лични данни',
          addresses: 'Адреси',
          employments: 'Месторабота',
          documentEducations: 'Образование',
          documentIds: 'Документи за самоличност',
          statuses: 'Състояния',
          docs: 'Документи',
          others: 'Други',
          inventory: 'Опис',
          applications: 'Заявления',
          examASs: 'Теор. изпити АС',
          reports: 'Отчети',
          examinationSystem: 'Изпитна система'
        },
        search: {
          names: 'Име',
          namesAlt: 'Име (алт.)',
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
          caseType: 'Тип дело'
        },
        reports: {
          tabs: {
            documents: 'Документи',
            licences: 'Лицензи',
            ratings: 'Квалификационни класове'
          }
        },
        reportDocuments: {
          lin: 'ЛИН',
          role: 'Документ (роля)',
          type: 'Тип документ',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDatePeriodFrom: 'Дата на издаване от',
          fromDatePeriodTo: 'Дата на издаване до',
          fromDate: 'Дата на издаване',
          toDatePeriodFrom: 'Дата на валидност от',
          toDatePeriodTo: 'Дата на валидност до',
          toDate: 'Дата на валидност',
          search: 'Търси',
          'export': 'Експорт',
          limitations: 'Ограничения',
          itemsPerPage: 'Бр. на стр.',
          medClass: 'Клас (на медицинско)'
        },
        reportLicences: {
          names: 'Име',
          lin: 'ЛИН',
          uin: 'ЕГН',
          fromDatePeriodFrom: 'Дата на издаване от',
          fromDatePeriodTo: 'Дата на издаване до',
          fromDate: 'Дата на издаване',
          toDatePeriodFrom: 'Дата на валидност от',
          toDatePeriodTo: 'Дата на валидност до',
          toDate: 'Дата на валидност',
          licenceTypeName: 'Тип лиценз',
          firstIssueDate: 'Дата на първо издаване',
          licenceAction: 'Основание',
          stampNumber: '№  на печат',
          licenceCode: '№',
          search: 'Търси',
          'export': 'Експорт',
          limitations: 'Ограничения',
          itemsPerPage: 'Бр. на стр.'
        },
        reportRatings: {
          lin: 'ЛИН',
          ratingTypeOrRatingLevel: 'Тип ВС, Степен <br>(раб. място)',
          ratingClass: 'Клас',
          authorization: 'Разрешение',
          aircraftTypeCategory: 'Категория',
          classOrCategory: 'Клас,<br>Подклас<br>(категория)',
          authorizationAndLimitations: 'Разрешение<br>(ограничения)',
          firstIssueDate: 'Първоначално издаване',
          fromDatePeriodFrom: 'Дата на издаване от',
          fromDatePeriodTo: 'Дата на издаване до',
          fromDate: 'Дата на издаване',
          toDatePeriodFrom: 'Дата на валидност от',
          toDatePeriodTo: 'Дата на валидност до',
          toDate: 'Дата на валидност',
          search: 'Търси',
          'export': 'Експорт',
          limitations: 'Ограничения',
          itemsPerPage: 'Бр. на стр.'
        },
        qlfStateDirective: {
          title: 'Създаване на състояние относно придибиване на квалификация',
          save: 'Запис',
          cancel: 'Отказ',
          fromDate: 'От дата',
          toDate: 'До дата',
          state: 'Състояние',
          stateStarted: 'Стартирал',
          stateCanceled: 'Прекратен',
          stateFinished: 'Приключил',
          qualification: 'Квалификация'
        },
        examDirective: {
          caseType: 'Тип дело',
          commonQuestions: 'Основни знания',
          specializedQuestions: 'Специализирани знания',
          examDate: 'Дата',
          inspectors: 'Инспектори',
          score: 'Точки',
          passed: 'Издържал',
          grade: 'Изчисляване',
          grading: 'Оценяване',
          extract: 'Извличане',
          successThreshold: 'Праг'
        },
        newExamAS: {
          title: 'Нов теоретичен изпит АС',
          save: 'Запис',
          cancel: 'Отказ'
        },
        searchExamAS: {
          examDate: 'Дата',
          commonQuestion: 'Основни знания',
          specializedQuestion: 'Специализирани знания',
          inspector: 'Инспектор',
          newExam: 'Нов теоретичен изпит АС',
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          caseType: 'Тип дело'
        },
        editExamAS: {
          title: 'Преглед на теоретичен изпит АС',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteExam: 'Изтрий'
        },
        examBatchNew: {
          title: 'Нов теоретичен изпит АС',
          title2: 'Теоретичен изпит АС',
          fileSource: 'Файл източник',
          fileName: 'Име на файл',
          extractPage: 'Раздели на страници',
          save: 'Запис и продължаване',
          edit: 'Редакция'
        },
        personDataDirective: {
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
          phones: 'Телефони',
          caseTypes: 'Типове дела'
        },
        inspectorDataDirective: {
          examinerCode: 'Идентификационен код',
          caa: 'Въздухоплавателна администрация',
          stampNum: 'Номер на печат',
          isValid: 'Валиден'
        },
        examinerDataDirective: {
          examinerCode: 'Идентификационен код',
          stampNum: 'Номер на печат',
          isValid: 'Валиден'
        },
        personReportDirective: {
          checksOfForeigners: 'Проверки на проверени чужденци',
          date: 'Дата',
          documentNumber: 'No на отчет',
          addCheck: 'Добави проверкa',
          addCheckOfForeigner: 'Добави проверкa на чужденец',
          includedChecks: ' Проверки',
          checksTable: {
            personLin: 'ЛИН',
            documentNumber: 'No док. на проверка',
            ratingClass: 'Клас',
            documentDateValidFrom: 'От дата',
            documentDateValidTo: 'До дата',
            authorization: 'Разре-<br>шение',
            licenceType: 'Вид право-<br>способност',
            personCheckDocumentType: 'Тип документ',
            personCheckDocumentRole: 'Роля на документ',
            ratingType: 'Тип ВС <br>(раб. място)',
            valid: 'Валидност',
            ratingValue: 'Оценка',
            noChecks: 'Няма налични проверки',
            names: 'Имена'
          },
          caseType: 'Тип дело'
        },
        personAddressDirective: {
          addressType: 'Вид',
          settlement: 'Населено място',
          address: 'Адрес',
          addressAlt: 'Адрес (латиница)',
          valid: 'Валиден',
          postalCode: 'Пощенски код',
          phone: 'Телефон'
        },
        personDocumentEducationDirective: {
          documentNumber: '№ на документ',
          completionDate: 'Дата на завършване',
          speciality: 'Специалност',
          graduation: 'Степен на образование',
          school: 'Учебно заведение',
          notes: 'Бележки',
          caseType: 'Тип дело'
        },
        personDocumentIdDirective: {
          caseType: 'Тип дело',
          personDocumentIdTypeId: 'Тип документ',
          valid: 'Валиден',
          documentNumber: '№ на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валиден до',
          documentPublisher: 'Издаден от',
          notes: 'Бележки'
        },
        personApplicationDirective: {
          title: 'Документът е приложен към заявления:',
          name: 'Име на заявление',
          number: '№ на заявление',
          view: 'Преглед'
        },
        personStatusDirective: {
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
          medClass: 'Клас',
          notes: 'Бележки',
          caseType: 'Тип дело'
        },
        personEmploymentDirective: {
          hiredate: 'Дата на назначаване',
          valid: 'Валиден',
          organization: 'Организация',
          employmentCategory: 'Категория длъжност',
          country: 'Страна',
          notes: 'Бележки',
          caseType: 'Тип дело'
        },
        personCheckDirective: {
          personCheckRatingValue: 'Оценка',
          checkData: 'Данни за проверка',
          report: 'Отчет, в който е включена тази проверка',
          reportDate: 'Дата на отчета',
          publisher: 'Издател',
          reportNumber: 'Nо на отчета'
        },
        personDocumentLangCertDirective: {
          langLevel: 'Ниво на език',
          langCertData: 'Данни за свидетелство за език',
          langLevelEntries: 'История на промени на ниво на език'
        },
        personFlyingExperienceDirective: {
          caseType: 'Тип дело',
          documentDate: 'Дата на документа',
          sender: 'Подател',
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
          dayDuration: 'Нальот(дневен)',
          nightDuration: 'Нальот(нощен)',
          IFR: 'IFR',
          VFR: 'VFR',
          dayLandings: 'Кацания(ден)',
          nightLandings: 'Кацания(нощ)',
          total: 'Общо количество (с натрупване)',
          totalDoc: 'Общо количество (по документа)',
          totalLastMonths: 'Общ нальот за посл. 12 месеца',
          notUnique: 'Данните се дублират с вече съществуващ запис',
          sum: 'Сумирай'
        },
        ratingEditionDirective: {
          documentDateValidFrom: 'Дата на вписване',
          documentDateValidTo: 'Валидно до',
          inspector: 'Инспектор',
          examiner: 'Проверяващ',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          limitations: 'Ограничения',
          subclasses: 'Подкласове',
          applications: 'Заявления'
        },
        ratingDirective: {
          staffType: 'Тип персонал',
          ratingType: 'Тип ВС',
          personRatingLevel: 'Степен',
          sector: 'Сектор/работно място',
          locationIndicator: 'Индикатор на местоположение',
          ratingClass: 'Клас',
          authorization: 'Разрешение',
          aircraftTypeGroup: 'Тип/Група ВС',
          ratingCategory: 'Категория',
          administration: 'Администрация',
          caseType: 'Тип дело'
        },
        licenceDirective: {
          caseType: 'Тип дело',
          licenceType: 'Вид',
          licenceNumber: 'Лиценз No',
          foreignLicenceNumber: 'Чужд лиценз No',
          valid: 'Действителен',
          lastLicenceNumber: 'Посл. издаден лиценз от избр. тип е с No',
          foreignPublisher: 'Чужда администрация',
          employment: 'Месторабота',
          publisher: 'Администрация'
        },
        licenceEditionDirective: {
          documentDateValidFrom: 'Дата на издаване',
          documentDateValidTo: 'Валидно до',
          inspector: 'Инспектор',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          limitations: 'Ограничения',
          AT_a_Ids: 'Aeroplanes Turbine A',
          AT_b1_Ids: 'Aeroplanes Turbine B1',
          AP_a_Ids: 'Aeroplanes Piston A',
          AP_b1_Ids: 'Aeroplanes Piston B1',
          HT_a_Ids: 'Helicopters Turbine A',
          HT_b1_Ids: 'Helicopters Turbine B1',
          HP_a_Ids: 'Helicopters Piston A',
          HP_b1_Ids: 'Helicopters Piston B1',
          avionics_Ids: 'Avionics B2',
          PE_b3_Ids: 'B3 Piston-engine non pressurised aeroplanes of 2 000 Kg MTOM and below',
          licenceAction: 'Основание',
          ratings: 'Квалификационни класове към лиценза',
          exams: 'Теоретични изпити към лиценза',
          langCerts: 'Свидетелства за език към лиценза',
          trainings: 'Обучения към лиценза',
          checks: 'Проверки към лиценза',
          meds: 'Медицински свидетелства към лиценза',
          licences: 'Лицензи',
          applications: 'Заявления'
        },
        personCommonDocDirective: {
          caseType: 'Тип дело',
          documentPublisher: 'Издател',
          valid: 'Валиден',
          documentNumber: 'Док No',
          documentNumberCheck: 'Док No на проверка',
          documentPersonNumber: 'No в списъка (групов док.)',
          groupDocNumber: 'Последен използван No в списъка',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          personDocumentType: 'Тип документ',
          personDocumentRole: 'Роля на документ',
          notes: 'Бележки',
          title: 'Общи данни'
        },
        personCommonDocClassificationDirective: {
          ratingType: 'Тип ВС',
          aircraftTypeGroup: 'Тип/Група ВС',
          ratingClass: 'Клас',
          authorization: 'Разрешение',
          licenceType: 'Вид правоспособност',
          locationIndicator: 'Индикатор за местоположение',
          sector: 'Сектор/работно място',
          title: 'Данни за класификация'
        },
        licenceEditionsEditChecksView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addExistingCheck: 'Съществуваща проверка',
          addCheck: 'Нова проверка',
          noChecks: 'Няма избрани проверки',
          availableChecks: 'Налични проверки',
          noAvailableChecks: 'Няма налични проверки',
          checksTable: {
            documentNumber: '№ на документа',
            ratingClass: 'Клас',
            documentDateValidFrom: 'От дата',
            documentDateValidTo: 'До дата',
            documentPublisher: 'Издател',
            authorization: 'Разре-<br>шение',
            licenceType: 'Вид право-<br>способност',
            personCheckDocumentType: 'Тип документ',
            personCheckDocumentRole: 'Роля на документ',
            ratingType: 'Тип ВС <br>(раб. място)',
            valid: 'Валидност',
            ratingValue: 'Оценка'
          }
        },
        licenceEditionsEditExamsView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addExam: 'Нов изпит',
          addExistingExam: 'Съществуващ изпит',
          noExams: 'Няма избрани изпити',
          examsTable: {
            documentNumber: '№ на документа',
            documentDateValidFrom: 'От дата',
            documentPublisher: 'Издател',
            valid: 'Валиден'
          }
        },
        licenceEditionsEditLangCertsView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addLangCert: 'Ново свидетелство',
          addExistingLangCert: 'Съществуващо свидетелство',
          noLangCerts: 'Няма избрани свидетелства',
          langCertsTable: {
            documentNumber: '№ на документа',
            langCertType: 'Тип',
            langLevel: 'Ниво',
            documentDateValidFrom: 'От дата',
            documentDateValidTo: 'До дата',
            documentPublisher: 'Издател',
            valid: 'Валиден'
          }
        },
        licenceEditionsEditLicencesView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          noLicences: 'Няма избрани лицензи',
          availableLicences: 'Налични лицензи',
          noAvailableLicences: 'Няма налични лицензи',
          addExistingLicence: 'Съществуващ лиценз',
          licencesTable: {
            licenceNumber: 'Лиценз No',
            licenceType: 'Наименование',
            firstEditionValidFrom: 'Първоначално издаване',
            documentDateValidFrom: 'Издаден',
            documentDateValidTo: 'Валиден до'
          }
        },
        licenceEditionsEditMedicalsView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addMed: 'Ново медицинско',
          addExistingMed: 'Съществуващо медицинско',
          noMeds: 'Няма избрани медицински свидетелства',
          availableMeds: 'Налични медицински свидетелства',
          noAvailableMeds: 'Няма налични медицински свидетелства',
          medsTable: {
            number: 'Свидетелство',
            dateValidFrom: 'От дата',
            dateValidTo: 'Валидно до',
            medClass: 'Клас',
            limitations: 'Ограничения',
            publisher: 'Издател'
          }
        },
        licenceEditionsEditRatingsView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addRating: 'Нов клас',
          addExistingRating: 'Съществуващ клас',
          noRatings: 'Няма избрани квалификации',
          availableRatings: 'Налични квалификации',
          noAvailableRatings: 'Няма налични квалификации',
          ratingsTable: {
            ratingType: 'Тип ВС, Степен (раб. място)',
            ratingClass: 'Клас (категория)',
            authorization: 'Разрешение <br>(ограничения)',
            dateValidFrom: 'Издаден',
            dateValidTo: 'Валиден до'
          }
        },
        licenceEditionsEditTrainingsView: {
          cancel: 'Отказ',
          changeOrder: 'Ред',
          saveOrder: 'Запис',
          addTraining: 'Ново обучение',
          addExistingTraining: 'Съществуващо обучение',
          noTrainings: 'Няма избрани обучения',
          availableTrainings: 'Налични обучения',
          noAvailableTrainings: 'Няма налични обучения',
          trainingsTable: {
            number: 'No',
            dateValidFrom: 'От дата',
            dateValidTo: 'Валидно до',
            publisher: 'Издател',
            ratingType: 'Тип ВС',
            docType: 'Тип',
            docRole: 'Роля'
          }
        },
        personDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          applicationCode: 'Код',
          documentDate: 'От дата',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Валута',
          taxAmount: 'Такса',
          file: 'Файл',
          stageName: 'Статус'
        },
        newPersonDocApplication: {
          title: 'Ново заявление',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editPersonDocApplication: {
          title: 'Преглед на заявление',
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция',
          deleteApplication: 'Изтрий',
          stage: 'Статус на заявлението'
        },
        newPerson: {
          title: 'Ново лице',
          personDataForm: 'Лични данни',
          documentIdForm: 'Документ за самоличност',
          inspDataTitle: 'Данни за инспектор',
          examinerDateTitle: 'Данни за проверяващ',
          addressForm: 'Адрес',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editPersonInfo: {
          title: 'Преглед на лични данни',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          inspDataTitle: 'Данни за инспектор',
          examinerDateTitle: 'Данни за проверяващ'
        },
        examinationSystem: {
            save: 'Запис',
            cancel: 'Отказ',
            edit: 'Редакция',
            title: 'Изпитна система',
            exSystExams: 'ИС: Изпити',
            qualificationName: 'Квалификация',
            state: 'Състояние',
            fromDate: 'От дата (включително)',
            toDate: 'До дата (включително)',
            exam: 'Тест',
            date: 'Дата на явяване',
            totalScore: 'Резултат',
            endTime: 'Край',
            status: 'Статус',
            newState: 'Ново състояние',
            certCamp: 'Сертификационна кампания',
            qualificationsTitle: 'Състояния относно придобиването на квалификации',
            addNewState: 'Добави ново състояние'
          },
        examinationSystemView: {
          checkConnection: 'Тест на връзката с изпитната с-ма.',
          loadData: 'Прехвърляне на данни от Изпитна система',
          updateStates:
            'Автоматизирана актуализация на състоянията относно придобиване на квалификация',
          tabs: {
            qualifications: 'Квалификации',
            exams: 'Тестове',
            certPaths: 'Сертификационни пътища',
            certCampaigns: 'Сертификационни кампании',
            examinees: 'Изпити'
          }
        },
        qualifications: {
          name: 'Наименование',
          code: 'Идентификационен код'
        },
        exSystExaminees: {
          uin: 'ЕГН',
          lin: 'ЛИН',
          result: 'Резултат',
          status: 'Статус',
          certCampaign: 'Сертификационна кампания',
          exam: 'Тест',
          endTime: 'Край'
        },
        exSystExams: {
          name: 'Наименование',
          code: 'Идентификационен код',
          qualificationName: 'Квалификация'
        },
        certCampaigns: {
          name: 'Наименование',
          code: 'Идентификационен код',
          validFrom: 'Валидна от',
          validTo: 'Валидна до',
          qualificationName: 'Квалификация'
        },
        certPaths: {
          name: 'Наименование',
          code: 'Идентификационен код',
          validFrom: 'Валидна от',
          validTo: 'Валидна до',
          qualificationName: 'Квалификация',
          exam: 'Тест'
        },
        stampedDocumentsView: {
          search: 'Търси',
          save: 'Запиши',
          licenceNumberSearch: 'No лиценз',
          licenceNumber: 'No',
          uin: 'ЕГН',
          lin: 'ЛИН',
          names: 'Име',
          documentDateValidFrom: 'Издаден',
          documentDateValidTo: 'Валиден до',
          licenceAction: 'Основание',
          ready: 'Готов',
          stampNumber: 'Хартиен №',
          application: 'Заявление',
          received: 'Получен',
          finished: 'Приключено заявление'
        },
        exportView: {
          tabs: {
            examsData: 'Заявления за изпити',
            personsData: 'Физически лица'
          }
        },
        exportPersonsData: {
          exportData: 'Експорт физически лица',
          addPersons: 'Добави физически лица',
          names: 'Име',
          uin: 'ЕГН',
          lin: 'ЛИН',
          chosenPersons: 'Избрани за експорт физически лица'
        },
        exportExamsData: {
          exportData: 'Експорт изпити',
          addExams: 'Добави изпити',
          chosenExams: 'Избрани за експорт изпити',
          documentNumber: 'No',
          documentDate: 'Дата',
          personLin: 'ЛИН',
          personUin: 'ЕГН',
          personNames: 'Заявител',
          stageName: 'Статус',
          examName: 'Тест',
          examDate: 'Дата на явяване на теста',
          certCampName: 'Kампания'
        },
        viewPerson: {
          name: 'Име',
          uin: 'ЕГН',
          lin: 'ЛИН',
          age: 'Възраст',
          company: 'Фирма',
          employmentCategory: 'Длъжност',
          caseType: 'Дело',
          edit: 'Редакция'
        },
        newReport: {
          title: 'Нов отчет',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editReport: {
          title: 'Преглед на отчет',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteReport: 'Изтрий'
        },
        reportSearch: {
          newReport: 'Нов отчет',
          reportNumber: 'No на отчета',
          date: 'Дата',
          file: 'Файл',
          checkedPersons: 'ЛИН-ове на проверени физически лица'
        },
        newAddress: {
          title: 'Нов адрес',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAddress: {
          title: 'Преглед на адрес',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteAddress: 'Изтрий'
        },
        addressSearch: {
          newAddress: 'Нов aдрес',
          type: 'Вид',
          settlement: 'Населено място',
          address: 'Адрес',
          addressAlt: 'Адрес (латиница)',
          postalCode: 'П.К.',
          phone: 'Телефон',
          valid: 'Актуален'
        },
        newStatus: {
          title: 'Ново състояние',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editStatus: {
          title: 'Преглед на състояние',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteStatus: 'Изтрий'
        },
        statusSearch: {
          newState: 'Ново Състояние',
          personStatusType: 'Причина',
          documentNumber: '№ на документа',
          documentDateValidFrom: 'Начална дата',
          documentDateValidTo: 'Крайна дата',
          notes: 'Бележки'
        },
        newDocumentId: {
          title: 'Нов документ за самоличност',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentId: {
          title: 'Преглед на документ за самоличност',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteDocId: 'Изтрий'
        },
        newDocumentEducation: {
          title: 'Ново обучение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentEducation: {
          title: 'Преглед на обучение',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteEdu: 'Изтрий'
        },
        documentIdSearch: {
          docTypeId: 'Документ',
          documentNumber: '№ на документа',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валиден до',
          publisher: 'Издаден от',
          valid: 'Валиден',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentId: 'Нов документ'
        },
        otherSearch: {
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка <br>(групов док.)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ',
          file: 'Файл'
        },
        newOther: {
          title: 'Нов документ',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOther: {
          title: 'Преглед на документ',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteOther: 'Изтрий'
        },
        medicalSearch: {
          testimonial: 'Свидетелство',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'Валидно до',
          medClass: 'Клас',
          limitations: 'Ограничения',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newMedical: 'Ново медицинско'
        },
        newMedical: {
          title: 'Ново свидетелство за медицинска годност',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editMedical: {
          title: 'Преглед на свидетелство за медицинска годност',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteMed: 'Изтрий'
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
          title: 'Нова месторабота',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editEmployment: {
          title: 'Преглед на месторабота',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteEmpl: 'Изтрий'
        },
        newExam: {
          title: 'Нов теоретичен изпит',
          save: 'Запис',
          cancel: 'Отказ'
        },
        searchExam: {
          documentNumber: '№ на документа',
          documentDateValidFrom: 'От дата',
          documentPublisher: 'Издател',
          valid: 'Валиден',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newExam: 'Нов теоретичен изпит'
        },
        editExam: {
          title: 'Преглед на теоретичен изпит',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteExam: 'Изтрий'
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
          authorization: 'Разре-<br>шение',
          licenceType: 'Вид право-<br>способност',
          personCheckDocumentType: 'Тип документ',
          personCheckDocumentRole: 'Роля на документ',
          ratingType: 'Тип ВС<br>(раб. място)',
          valid: 'Валид-<br>ност',
          ratingValue: 'Оценка',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Бр. стр.',
          file: 'Файл'
        },
        editCheck: {
          title: 'Преглед на проверка',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCheck: 'Изтрий'
        },
        newCheck: {
          title: 'Нова проверка',
          save: 'Запис',
          cancel: 'Отказ'
        },
        documentTrainingSearch: {
          staffType: 'Тип персонал',
          documentNumber: '№ на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          ratingType: 'Тип ВС<br>(раб. място)',
          ratingClass: 'Клас',
          authorization: 'Разре-<br>шение',
          licenceType: 'Вид право-<br>способност',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля на документ',
          valid: 'Валидно',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentTraining: 'Нов документ'
        },
        newDocumentTraining: {
          title: 'Ново обучение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDocumentTraining: {
          title: 'Преглед на обучение',
          save: 'Запис',
          edit: 'Редакция',
          cancel: 'Отказ',
          deleteTraining: 'Изтрий'
        },
        documentLangCertSearch: {
          staffType: 'Тип персонал',
          documentNumber: '№ на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          ratingType: 'Тип ВС<br>(раб. място)',
          ratingClass: 'Клас',
          authorization: 'Разре-<br>шение',
          licenceType: 'Вид право-<br>способност',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля на документ',
          valid: 'Валидно',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          file: 'Файл',
          newDocumentLangCert: 'Нов документ',
          langLevel: 'Ниво на език'
        },
        newDocumentLangCert: {
          title: 'Ново свидетелство за език',
          save: 'Запис',
          cancel: 'Отказ',
          langLevel: 'Ниво на език',
          changeDate: 'Дата'
        },
        editDocumentLangCert: {
          title: 'Преглед на свидетелство за език',
          save: 'Запис',
          edit: 'Редакция',
          cancel: 'Отказ',
          deleteLangCert: 'Изтрий'
        },
        flyingExperienceSearch: {
          staffType: 'Тип персонал',
          organization: 'Организация',
          ratingType: 'Тип ВС<br>(раб. място)',
          ratingClass: 'Клас',
          licenceType: 'Вид право-<br>способност',
          authorization: 'Разре-<br>шение',
          locationIndicator: 'Местоположение',
          sector: 'Сектор/работно място',
          experienceRole: 'Роля',
          experienceMeasure: 'Мерна единица',
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
          title: 'Нов летателен/практически опит',
          save: 'Запис',
          cancel: 'Отказ'
        },
        flyingExperienceEdit: {
          title: 'Преглед на летателен/практически опит',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteFlyingExp: 'Изтрий'
        },
        inventorySearch: {
          caseType: 'Тип дело',
          bookPageNumber: '№ на стр.',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл',
          notIndexed: 'Документи извън описа'
        },
        ratingSearch: {
          newRating: 'Нов клас',
          ratingTypeOrRatingLevel: 'Тип ВС, Степен <br>(раб. място)',
          classOrCategory: 'Клас,<br>Подклас<br>(категория)',
          authorizationAndLimitations: 'Разрешение<br>(ограничения)',
          firstEditionValidFrom: 'Първоначално издаване',
          lastEditionValidFrom: 'Издаден',
          lastEditionValidTo: 'Валиден до',
          lastEditionNotes: 'Бележки',
          lastEditionNotesAlt: 'Бележки лат.'
        },
        ratingEdit: {
          title: 'Преглед на квалификация',
          edit: 'Редакция',
          newEdition: 'Ново вписване',
          editLastEdition: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        ratingNew: {
          title: 'Нова квалификация',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newRatingEdition: {
          title: 'Ново вписване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRatingEdition: {
          editionForm: 'Редакция на вписване',
          editions: 'Вписвания',
          ratingTypeOrRatingLevel: 'Тип ВС/Степен <br>(раб.място)',
          classOrCategory: 'Клас/Подклас (Категория)',
          authorizationAndLimitations: 'Разрешение <br>(ограничения)',
          firstEditionValidFrom: 'Първоначално издаване',
          documentDateValidFrom: 'Дата на издаване',
          documentDateValidTo: 'Валиден до',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          inspectorExaminer: 'Инспектор/Проверяващ',
          edit: 'Редакция',
          deleteEdition: 'Изтриване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseRatingsModal: {
          title: 'Избор на квалификации',
          ratingTypeOrRatingLevel: 'Тип ВС, Степен (раб.място)',
          classOrCategory: 'Клас (категория)',
          authorizationAndLimitations: 'Разрешение <br>(ограничения)',
          lastEditionValidFrom: 'Издаден',
          lastEditionValidTo: 'Валиден до',
          showAll: 'Покажи всички',
          add: 'Добави',
          cancel: 'Отказ'
        },
        newRatingModal: {
          title: 'Нова квалификация',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseLangCertsModal: {
          title: 'Избор на свидетелства за език',
          documentNumber: 'No на свидетелството',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          langCertType: 'Тип',
          langLevel: 'Ниво',
          documentPublisher: 'Издател',
          valid: 'Валиден',
          showAll: 'Покажи всички',
          add: 'Добави',
          cancel: 'Отказ',
          errorMessage: 'Не може в едно вписване да има две свидетелства за един език.'
        },
        newLangCertModal: {
          title: 'Нова свидетелство за език',
          save: 'Запис',
          cancel: 'Отказ',
          langLevel: 'Ниво на език',
          changeDate: 'Дата',
          langCertData: 'Данни за ниво на език'
        },
        langLevelEntriesModal: {
          title: 'Промяна на нивото на език',
          add: 'Добави',
          cancel: 'Отказ',
          langLevel: 'Ниво на език',
          newLangLevelChange: 'Ново ниво',
          changeDate: 'Дата на промяна',
          langLevelEntries: 'Промени на нивото на език'
        },
        chooseExamsModal: {
          title: 'Избор на теоретичен изпит',
          documentNumber: '№ на документа',
          documentDateValidFrom: 'От дата',
          documentPublisher: 'Издател',
          valid: 'Валиден',
          add: 'Добави',
          cancel: 'Отказ'
        },
        newExamModal: {
          title: 'Нов теоретичен изпит',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseTrainingsModal: {
          title: 'Избор на обучения',
          number: 'No',
          dateValidFrom: 'От дата',
          dateValidTo: 'Валидно до',
          publisher: 'Издател',
          ratingType: 'Тип ВС',
          docType: 'Тип',
          docRole: 'Роля',
          add: 'Добави',
          cancel: 'Отказ'
        },
        newTrainingModal: {
          title: 'Ново обучение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseChecksModal: {
          title: 'Избор на проверки',
          documentNumber: '№ на документа',
          personCheckDocumentType: 'Тип документ',
          personCheckDocumentRole: 'Роля на документ',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          ratingType: 'Тип ВС<br>(раб. място)',
          ratingClass: 'Клас',
          authorization: 'Разре-<br>шение',
          licenceType: 'Вид право-<br>способност',
          valid: 'Валидност',
          ratingValue: 'Оценка',
          add: 'Добави',
          cancel: 'Отказ'
        },
        newCheckModal: {
          title: 'Нова проверка',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newCheckOfForeignerModal: {
          title: 'Нова проверка на чужденец',
          save: 'Запис',
          cancel: 'Отказ',
          ratingTypes: 'Тип ВС',
          ratingClass: 'Клас',
          names: 'Имена',
          documentDate: 'Дата',
          documentType: 'Тип документ',
          documentNumber: '№ на документа'
        },
        newQlfStateModal: {
          save: 'Запис',
          cancel: 'Отказ',
          title: 'Ново състояние'
        },
        choosePersonModal: {
          title: 'Избор на физическо лице',
          lin: 'ЛИН',
          names: 'Име',
          add: 'Добави'
        },
        choosePersonsModal: {
          title: 'Избор на физически лица',
          add: 'Добави',
          cancel: 'Отказ',
          lin: 'ЛИН',
          names: 'Име',
          uin: 'ЕГН'
        },
        chooseAppExamsModal: {
          title: 'Избор на изпити',
          add: 'Добави',
          cancel: 'Отказ',
          documentNumber: 'No',
          documentDate: 'Дата',
          personLin: 'ЛИН',
          personUin: 'ЕГН',
          personNames: 'Заявител',
          stageName: 'Статус',
          examName: 'Тест',
          examDate: 'Дата на явяване на изпита',
          certCampName: 'Kампания'
        },
        chooseMedicalsModal: {
          title: 'Избор на медицински',
          number: 'Свидетелство',
          dateValidFrom: 'От дата',
          dateValidTo: 'Валидно до',
          medClass: 'Клас',
          limitations: 'Ограничения',
          publisher: 'Издател',
          add: 'Добави',
          cancel: 'Отказ'
        },
        newMedicalModal: {
          title: 'Нова медицинско',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseLicencesModal: {
          title: 'Избор на лицензи',
          licenceNumber: 'Лиценз No',
          licenceType: 'Наименование',
          firstEditionValidFrom: 'Първоначално издаване',
          documentDateValidFrom: 'Издаден',
          documentDateValidTo: 'Валиден до',
          add: 'Добави',
          cancel: 'Отказ'
        },
        licenceEditionDocModal : {
          title: 'Редакция на документ към вписване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        printLicenceModal: {
          title: 'Принтиране на лиценз',
          stampNumber: 'Хартиен №',
          cancel: 'Отказ',
          save: 'Запис',
          print: 'Преглед за печат',
          generateNew: 'Прегенерирай',
          edit: 'Редакция'
        },
        printRatingEditionModal: {
          title: 'Принтиране на квалификационен клас',
          close: 'Затвори',
          print: 'Преглед за печат',
          generateNew: 'Прегенерирай'
        },
        changeCaseTypeModal: {
          title: 'Промяна на типа дело',
          cancel: 'Отказ',
          select: 'Избери',
          name: 'Наименование'
        },
        licencesSearch: {
          newLicence: 'Нов лиценз',
          licenceNumber: 'Лиценз No',
          licenceType: 'Наименование',
          firstEditionValidFrom: 'Първ. издаване',
          documentDateValidFrom: 'Издаден',
          documentDateValidTo: 'Валиден до',
          licenceAction: 'Осн.',
          statusChange: 'Пром. на статуса',
          notes: 'Бележки',
          inspector: 'Инспектор',
          application: 'Заявление',
          foreignLicence: 'Чужд лиценз No / Админ',
          valid: 'Валидност',
          pageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          limitations: 'Ограничения',
          file: 'Файл'
        },
        newLicence: {
          title: 'Нов лиценз',
          save: 'Запис',
          cancel: 'Отказ'
        },
        licenceView: {
          title: 'Лиценз',
          statusChanges: 'Промени на статуса на лиценза',
          valid2: 'Валидност',
          changeDate: 'Дата на промяна',
          changeReason: 'Причина за промяна',
          inspector: 'Инспектор',
          notes: 'Бележки',
          valid: 'Действителен',
          newEdition: 'Ново вписване',
          tabs: {
            ratings: 'Квалификации',
            exams: 'Теоретични изпити',
            langCerts: 'Свидетелства за език',
            trainings: 'Обучения',
            checks: 'Проверки',
            medicals: 'Медицински',
            licences: 'Лицензи'
          }
        },
        editLicenceEditions: {
          title: 'Преглед на вписвания',
          editionForm: 'Редакция на вписване',
          editions: 'Вписвания',
          limitations: 'Ограничения',
          documentDateValidFrom: 'Издаден',
          documentDateValidTo: 'Валиден до',
          inspector: 'Инспектор',
          application: 'Заявление',
          stampNumber: 'Хартиен №',
          notes: 'Бележки',
          notesAlt: 'Бележки лат.',
          reason: 'Основание',
          bookPageNumber: '№ стр. в<br>делов. книга',
          pageCount: 'Брой стр.',
          licenceAction: 'Основание',
          edit: 'Редакция',
          deleteEdition: 'Изтриване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newLicenceEdition: {
          title: 'Ново вписване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        licenceStatus: {
          valid: 'Валидност',
          changeDate: 'Дата на промяна',
          changeReason: 'Причина за промяна',
          inspector: 'Инспектор',
          notes: 'Бележки'
        },
        licenceStatusesModal: {
          title: 'Статус на лиценз',
          newStatusChange: 'Нова промяна на статуса на лиценз',
          statusChanges: 'Промени на статуса на лиценза',
          valid: 'Валидност',
          changeDate: 'Дата на промяна',
          changeReason: 'Причина за промяна',
          inspector: 'Инспектор',
          notes: 'Бележки',
          save: 'Запис',
          cancel: 'Отказ'
        },
        printableDocsSearch: {
          lin: 'ЛИН',
          uin: 'ЕГН',
          names: 'Имена',
          licenceAction: 'Основание',
          licenceType: 'Вид лиценз',
          licenceNumber: 'Лиценз No',
          dateValidFrom: 'Дата на издаване',
          dateValidTo: 'Валиден до',
          application: 'Заявление',
          search: 'Търси'
        }
      },
      applications: {
        lotSet: 'Тип на дело',
        tabs: {
          'case': 'Преписка',
          stages: 'Дейности по заявление',
          data: 'Данни за заявление',
          examSyst: 'Изпитна система'
        },
        appExSystDataDirective: {
            date: 'Дата на явяване',
            exam: 'Тест',
            addExams: 'Към промяна на списък с тестове от заявлението',
            certCampaign: 'Сертификационна кампания',
            schools: 'Учебен център',
            exSystExams: 'ИС: Тестове',
            noEntry: 'Няма информация за тестове'
        },
        edit: {
          equipmentName: 'Име',
          equipmentType: 'Тип',
          equipmentProducer: 'Производител',
          airportName: 'Име',
          airportType: 'Тип',
          aircraftProducer: 'Производител',
          aircraftCategory: 'AIR категория',
          aircraftICAO: 'ICAO',
          organizationName: 'Име',
          organizationCAO: 'САО',
          organizationUIN: 'Булстат',
          personName: 'Име',
          personLin: 'ЛИН',
          status: 'Статус',
          docTypeName: 'Относно',
          viewPerson: 'Преглед',
          viewOrganization: 'Преглед',
          viewAircraft: 'Преглед',
          viewAirport: 'Преглед',
          viewEquipment: 'Преглед',
          unlinkedLotParts: 'Документи извън преписката',
          'case': {
            regNumber: 'Тип/№/Дата',
            description: 'Тип',
            act: 'Дело',
            viewDoc: 'Преглед',
            childDoc: 'Подчинен документ',
            page: 'стр.',
            linkNew: 'Добави страница',
            linkPart: 'Свържи със страница',
            newFile: 'Нов файл и страница',
            newDocFile: 'Нов файл',
            viewPart: 'Преглед',
            linkApplication: 'Свържи заявление'
          },
          stages: {
            docStages: 'Деловодни етапи',
            endElectronicStage: 'Приключване',
            nextElectronicStage: 'Следващ',
            techEditElectronicStage: 'Техн. редакция',
            reverseElectronicStage: 'Сторниране',
            startingDate: 'Нач. дата',
            esStageName: 'Етап',
            esStageExecutors: 'Изпълнител',
            expectedEndingDate: 'Очаквана дата на прикл.',
            endingDate: 'Дата на приключване',
            isCurrentStage: 'Текущ',
            noAvailableDocStages: 'Няма намерени резултати',
            edit: 'Техническа редакция на етап',
            end: 'Приключване на етап',
            next: 'Следващ етап',
            appStages: 'Дейности по заявление',
            newAppStage: 'Нова дейност',
            appStage: 'Дейност',
            appStageDate: 'Дата',
            appStageInspector: 'Инспектор',
            note: 'Бележка'
          },
          newFile: {
            title: 'Нова страница в описа',
            documentType: 'Тип на документ',
            caseType: 'Тип дело',
            cancel: 'Отказ',
            addPart: 'Продължи'
          },
          newChildDoc: {
            title: 'Нов подчинен документ',
            register: 'Регистрирай',
            cancel: 'Отказ',
            caseRegUri: 'Към преписка',
            docCorrespondent: 'Кореспондент'
          },
          newDocFile: {
            title: 'Нов файл',
            cancel: 'Отказ',
            save: 'Запис',
            name: 'Наименование',
            docFileKind: 'Вид файл',
            file: 'Прикачен файл'
          },
          linkFile: {
            title: 'Свържи документ в описа',
            documentType: 'Тип на документ',
            search: 'Търси',
            cancel: 'Отказ',
            select: 'Избор'
          },
          addPart: {
            title: 'Нова страница в описа',
            cancel: 'Отказ',
            save: 'Запис'
          },
          unlinkedParts: {
            setPartName: 'Тип страница',
            gvaCaseTypeName: 'Тип дело',
            pageNumber: '№ стр. в дело',
            pageIndex: 'Брой стр.',
            file: 'Файл',
            viewPart: 'Преглед'
          },
          examinationSystem: {
            title: 'Данни за изпити',
            save: 'Запис',
            cancel: 'Отказ',
            edit: 'Редакция',
            qualificationsTitle: 'Състояния относно придобиването на квалификации',
            qualificationName: 'ИС: Квалификация',
            status: 'Статус',
            licenceType: 'Вид правоспособност'
          }
        },
        newForm: {
          title: 'Ново заявление',
          caseType: 'Тип дело',
          applicationType: 'Тип заяление',
          docCorrespondent: 'Кореспондент',
          newCorr: 'Нов кореспондент',
          register: 'Регистрирай',
          cancel: 'Отказ'
        },
        editAppPart: {
          title: 'Преглед на заявление',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        equipmentSelect: {
          equipment: 'Съоръжение',
          newEquipment: 'Ново съоръжение'
        },
        chooseEquipmentModal: {
          title: 'Избор на съоръжение',
          cancel: 'Отказ',
          name: 'Наименование',
          search: 'Търси',
          select: 'Избери',
          equipmentType: 'Тип',
          equipmentProducer: 'Производител',
          manPlace: 'Място на производство',
          manDate: 'Дата на производство',
          place: 'Местоположение',
          operationalDate: 'Дата на въвеждане в експлоатация'
        },
        newEquipmentModal: {
          title: 'Ново съоръжение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        airportSelect: {
          airport: 'Летище',
          newAirport: 'Ново летище'
        },
        chooseAirportModal: {
          title: 'Избор на летище',
          cancel: 'Отказ',
          name: 'Наименование',
          icao: 'ICAO код',
          search: 'Търси',
          select: 'Избери',
          airportType: 'Тип',
          place: 'Местоположение',
          runway: 'Полоса',
          course: 'Курс',
          excess: 'Превишение ',
          concrete: 'Полоса-бетон'
        },
        newAirportModal: {
          title: 'Ново летище',
          save: 'Запис',
          cancel: 'Отказ'
        },
        aircraftSelect: {
          aircraft: 'Въздухоплавателно средство',
          newAircraft: 'Ново ВС'
        },
        chooseAircraftModal: {
          title: 'Избор на въздухоплавателно средство',
          cancel: 'Отказ',
          manSN: 'Сериен номер',
          model: 'Модел',
          mark: 'Рег. знак',
          search: 'Търси',
          select: 'Избери',
          outputDate: 'Дата на производство',
          aircraftCategory: 'Тип ВС',
          aircraftProducer: 'Производител',
          engine: 'Двигател',
          propeller: 'Витло',
          modifOrWingColor: 'Модификация/Цвят на крило'
        },
        newAircraftModal: {
          title: 'Ново въздухоплавателно средство',
          save: 'Запис',
          cancel: 'Отказ'
        },
        addCaseTypesModal: {
          title: 'Добавяне на типове дела към физическо лице',
          save: 'Запис',
          cancel: 'Отказ',
          name: 'Наименование'
        },
        addExamsModal: {
          title: 'Редакция на тестове',
          save: 'Запис',
          cancel: 'Отказ',
          date: 'Дата на явяване',
          exam: 'Тест',
          noEntry: 'Няма информация за тестове'
        },
        organizationSelect: {
          organization: 'Организация',
          newOrganization: 'Нова организация'
        },
        chooseOrganizationModal: {
          title: 'Избор на организация',
          cancel: 'Отказ',
          name: 'Наименование',
          uin: 'БУЛСТАТ',
          cao: 'САО',
          dateValidTo: 'Валидност до',
          dateCaoValidTo: 'САО - дата на валидност',
          search: 'Търси',
          select: 'Избери',
          organizationType: 'Тип организация',
          valid: 'Валидност'
        },
        newOrganizationModal: {
          title: 'Нова организация',
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseAppTypeModal: {
          title: 'Избор на вид заявление',
          cancel: 'Отказ',
          select: 'Избери',
          name: 'Наименование',
          code: 'Код',
          search: 'Търси'
        },
        corrSelect: {
          correspondent: 'Кореспондент',
          newCorr: 'Нов кореспондент',
          displayName: 'Наименование',
          email: 'Имейл',
          correspondentType: 'Тип',
          search: 'Търси',
          cancel: 'Назад',
          select: 'Избор'
        },
        corrNew: {
          title: 'Нов кореспондент',
          saveAndSelect: 'Запис и избор',
          cancel: 'Отказ'
        },
        docSelect: {
          regDate: 'Дата',
          regUri: 'Рег.№',
          docSubject: 'Относно',
          docDirectionName: '',
          docStatusName: 'Статус',
          correspondentName: 'Кореспондент'
        },
        chooseDocsModal: {
          title: 'Избор на документ',
          cancel: 'Отказ',
          fromDate: 'От дата',
          toDate: 'До дата',
          regUri: 'Рег.№',
          docName: 'Относно',
          docType: 'Вид на документа',
          docStatus: 'Статус на документа',
          corrs: 'Кореспонденти',
          units: 'Отнесено към',
          search: 'Търси',
          select: 'Избери',
          regDate: 'Дата',
          docSubject: 'Относно',
          docDirectionName: '',
          docStatusName: 'Статус',
          correspondentName: 'Кореспондент'
        },
        search: {
          fromDate: 'От дата',
          toDate: 'До дата',
          search: 'Търси',
          newApp: 'Ново',
          lotSetName: 'Партида',
          documentNumber: '№ на документ',
          applicationType: 'Тип заявление',
          documentDate: 'От дата',
          status: 'Статус',
          lin: 'ЛИН',
          description: 'Описание',
          stage: 'Статус',
          stageTermDate: 'Срок',
          aircraftICAO: 'ICAO',
          organizationUIN: 'Булстат',
          inspector: 'Инспектор'
        },
        applicationDocument: {
          title: 'Сканиран (електронен) документ',
          name: 'Наименование',
          fileKind: 'Вид файл',
          fileType: 'Тип файл',
          caseType: 'Тип дело',
          pageIndex: '№ стр.',
          pageNumber: 'Брой стр.',
          attachment: 'Прикачен файл'
        },
        editDocStageModal: {
          stage: 'Етап',
          startingDate: 'Начална дата',
          executors: 'Изпълнители',
          expectedEndingDate: 'Очаквана дата прикл.',
          endingDate: 'Дата на приключване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        endDocStageModal: {
          stage: 'Етап',
          startingDate: 'Начална дата',
          executors: 'Изпълнители',
          expectedEndingDate: 'Очаквана дата прикл.',
          endingDate: 'Дата на приключване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        nextDocStageModal: {
          stage: 'Етап',
          startingDate: 'Начална дата',
          executors: 'Изпълнители',
          expectedEndingDate: 'Очаквана дата прикл.',
          endingDate: 'Дата на приключване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        appStage: {
          stage: 'Дейност',
          date: 'Дата',
          inspector: 'Инспектор',
          note: 'Бележка'
        },
        newAppStageModal: {
          title: 'Нова дейност',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAppStageModal: {
          title: 'Преглед на дейност',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteStage: 'Изтрий'
        }
      },
      organizations: {
        tabs: {
          docs: 'Документи',
          others: 'Други',
          approvals: 'Удостоверения за одобрение',
          inspections: 'Одити и надзор',
          inspection: 'Одит',
          recommendations: 'Доклад от препоръки',
          staff: 'Персонал',
          staffManagement: 'Ръководен персонал',
          awExaminers: 'Проверяващи ЛГ',
          addresses: 'Адреси',
          airportOperator: 'Летищен оператор',
          certAirportOperators: 'Лиценз',
          groundServiceOperators: 'Оператор по наземно обслужване',
          airOperator: 'Авиационен оператор',
          certAirOperators: 'Свидетелство',
          airCarrier: 'Въздушен превозвач',
          certAirCarriers: 'Лиценз',
          airNavigationServiceDeliverer: 'Доставчик АО',
          certAirNavigationServiceDeliverer: 'Свидетелство',
          certGroundServiceOperators: 'Лиценз',
          groundServiceOperatorsSnoOperational: 'Удостоверение за експлоатационна годност',
          inventory: 'Опис',
          applications: 'Заявления'
        },
        search: {
          uin: 'Булстат',
          newOrganization: 'Нова организация',
          search: 'Търси',
          name: 'Наименование',
          caseType: 'Тип дело',
          cao: 'САО',
          valid: 'Валидност',
          organizationType: 'Тип организация',
          dateValidTo: 'Валидност до',
          dateCaoValidTo: 'САО - дата на валидност'
        },
        viewOrganization: {
          name: 'Наименование',
          cao: 'САО',
          uin: 'Булстат',
          organizationType: 'Тип организация',
          edit: 'Редакция'
        },
        newOrganization: {
          title: 'Нова организация',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOrganization: {
          title: 'Преглед на данни за организация',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        inventorySearch: {
          bookPageNumber: '№ на стр.',
          document: 'Документ',
          type: 'Вид',
          docNumber: '№ на документа',
          date: 'Дата',
          publisher: 'Издател',
          valid: 'Валиден',
          fromDate: 'От дата',
          toDate: 'До дата',
          pageCount: 'Бр. стр.',
          file: 'Файл',
          notIndexed: 'Документи извън описа'
        },
        chooseDocumentsModal: {
          title: 'Избор на документи',
          add: 'Добави',
          cancel: 'Отказ',
          search: 'Търси',
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
        chooseLimitationModal: {
          title: 'Избор на ограничение',
          cancel: 'Отказ',
          name: 'Наименование',
          select: 'Избери'
        },
        chooseEmploymentModal: {
          title: 'Избор на длъжност',
          name: 'Наименование',
          select: 'Избери',
          cancel: 'Отказ'
        },
        chooseInspectionsModal: {
          title: 'Избор на одити',
          add: 'Запиши',
          cancel: 'Отказ',
          documentNumber: '№ на документ',
          subject: 'Предмет на одит',
          inspectionFrom: 'Начална дата на изпълнение на одита',
          inspectionTo: 'Крайна дата на изпълнение на одита'
        },
        chooseInspectorsModal: {
          title: 'Избор на инспектор',
          name: 'Име',
          cancel: 'Отказ',
          add: 'Добави'
        },
        editCertAirportOperator: {
          title: 'Преглед на лиценз на летищен оператор',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertOp: 'Изтрий'
        },
        newCertAirportOperator: {
          title: 'Нов лиценз на летищен оператор',
          save: 'Запис',
          cancel: 'Отказ'
        },
        certAirportOperatorSearch: {
          newCertAirportOperator: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          airport: 'Летище',
          activities: 'Дейности'
        },
        editCertAirOperator: {
          title: 'Преглед на свидетелство на авиационен оператор',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertOp: 'Изтрий'
        },
        newCertAirOperator: {
          title: 'Ново свидетелство на авиационен оператор',
          save: 'Запис',
          cancel: 'Отказ'
        },
        certAirOperatorSearch: {
          newCertAirOperator: 'Ново свидетелство',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          specs: 'Спецификации'
        },
        editCertAirNavigationServiceDeliverer: {
          title: 'Преглед на свидетелство на доставчик на аеронавигационно обслужване',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertOp: 'Изтрий'
        },
        newCertAirNavigationServiceDeliverer: {
          title: 'Ново свидетелство на доставчик на аеронавигационно обслужване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        certAirNavigationServiceDelivererSearch: {
          newCertAirNavigationServiceDeliverer: 'Ново свидетелство',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          specs: 'Спецификации'
        },
        newAddress: {
          title: 'Нов адрес',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAddress: {
          title: 'Преглед на адрес',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteAddress: 'Изтрий'
        },
        addressSearch: {
          newAddress: 'Нов aдрес',
          addressType: 'Тип',
          valid: 'Валидно',
          settlement: 'Населено място',
          address: 'Адрес',
          addressAlt: ' Адрес на поддържащ език',
          phone: 'Телефон(и) за връзка на този адрес',
          fax: 'Факс',
          postalCode: 'Пощенски код',
          contactPerson: 'Лице за контакти',
          email: 'E-мейл адрес'
        },
        staffManagementSearch: {
          newStaffManagement: 'Нов ръководен персонал',
          position: 'Предлагана длъжност',
          person: 'Предложено лице',
          testDate: 'Дата на писмен тест',
          testScore: 'Оценка',
          valid: 'Валиден',
          application: 'Преписка (Заявление)'
        },
        newStaffManagement: {
          title: 'Нов ръководен персонал',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editStaffManagement: {
          title: 'Преглед на ръководен персонал',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteStaffMng: 'Изтрий'
        },
        awExaminerSearch: {
          newAwExaminer: 'Нов проверяващ',
          code: 'Идентификационен код',
          valid: 'Валиден',
          person: 'Физическо лице',
          stampNumber: 'Хартиен №'
        },
        newAwExaminer: {
          title: 'Нов проверяващ',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAwExaminer: {
          title: 'Преглед на проверяващ',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteStaffChecker: 'Изтрий'
        },
        otherSearch: {
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка <br>(групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          organizationOtherDocumentType: 'Тип документ',
          organizationOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          newOther: 'Нов документ',
          file: 'Файл'
        },
        newOther: {
          title: 'Нов документ',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOther: {
          title: 'Преглед на документ',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteDocument: 'Изтрий'
        },
        certGroundServiceOperatorSnoOperationalSearch: {
          newCertGroundServiceOperatorSnoOperational: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          airport: 'Летище'
        },
        newCertGroundServiceOperatorsSnoOperational: {
          title: 'Ново удостоверение за експлоатационна годност',
          save: 'Запис',
          cancel: 'Отказ',
          equipmentForm: 'Съоръжения'
        },
        editCertGroundServiceOperatorsSnoOperational: {
          title: 'Преглед на удостоверение за експлоатационна годност',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertGround: 'Изтрий',
          equipmentForm: 'Съоръжения'
        },
        certGroundServiceOperatorSearch: {
          newCertGroundServiceOperator: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          airport: 'Летище',
          activities: 'Дейности'
        },
        newCertGroundServiceOperator: {
          title: 'Нов лиценз на оператор по наземно обслужване / самообслужване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editCertGroundServiceOperator: {
          title: 'Преглед на лиценз на оператор по наземно обслужване / самообслужване',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertOp: 'Изтрий'
        },
        certAirCarrierSearch: {
          newCertAirCarrier: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          aircarrierServices: 'Предлагани услуги'
        },
        newCertAirCarrier: {
          title: 'Нов лиценз на оператор на въздушен превозвач',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editCertAirCarrier: {
          title: 'Преглед на лиценз на оператор на въздушен превозвач',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteCertOp: 'Изтрий'
        },
        inspectionSearch: {
          newInspection: 'Нов одит',
          documentNumber: '№ на документ',
          auditState: 'Състояние',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          subject: 'Предмет на одит',
          notification: 'Оператора предварително уведомен',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          inspectionPlace: 'Адрес на одитирания обект',
          application: 'Преписка (Заявление)'
        },
        newInspection: {
          title: 'Нов одит за организация',
          save: 'Запис',
          cancel: 'Отказ',
          caseType: 'Дело'
        },
        editInspection: {
          title: 'Преглед на одит за организация',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteInspection: 'Изтрий',
          recommendations: 'Доклади от препоръки свързани с одит:',
          auditPart: 'Тип',
          formText: '№ на изменението',
          formDate: 'Форма за заявен обхват на одобрението от дата'
        },
        approvalSearch: {
          newApproval: 'Ново удостоверение',
          approvalType: 'Тип одобрение',
          documentNumber: ' Номер на одобрението',
          documentNumberAmendment: 'Референтен № на описание',
          documentFirstDateIssue: 'Дата на първо издаване',
          documentDateIssueAmendment: 'Дата на изменение',
          changeNumAmendment: 'Номер на изменение',
          approvalState: 'Състояние',
          application: 'Преписка (Заявление)'
        },
        approvalNew: {
          title: 'Ново удостоверение за одобрение',
          amendmentTitle: 'Изменение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        newApprovalAmendment: {
          title: 'Ново изменение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editApprovalAmendments: {
          amendmentForm: 'Редакция на изменение',
          amendmentTitle: 'Изменение',
          approvalForm: 'Удостоверение за одобрение',
          amendments: 'Изменения',
          editLastAmendment: 'Редакция',
          deleteLastAmendment: 'Изтриване',
          approvalType: 'Тип одобрение',
          documentNumber: ' Номер на одобрението',
          documentFirstDateIssue: 'Дата на първо издаване',
          documentDateIssueAmendment: 'Дата на изменение',
          changeNumAmendment: 'Номер на изменение',
          approvalState: 'Състояние',
          documentNumberAmendment: 'Референтен № на описание на изменение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        approvalEdit: {
          title: 'Преглед на одобрение',
          newAmendment: 'Ново изменение',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        organizationOtherDirective: {
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          organizationOtherDocumentType: 'Тип документ',
          organizationOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          caseType: 'Тип дело'
        },
        recommendationSearch: {
          newRecommendation: 'Нов доклад',
          auditPart: 'Част',
          formText: '№ на изменението',
          interviewedStaff: 'Интервюиран персонал',
          finished1Date: 'Прикл. Част 1',
          finished2Date: 'Прикл. Част 2',
          finished3Date: 'Прикл. Част 3',
          finished4Date: 'Прикл. Част 4',
          finished5Date: 'Прикл. Част 5',
          recommendation: 'Препоръки',
          documentDescription : 'Описание'
        },
        newRecommendation: {
          title: 'Нов доклад от препоръки',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRecommendation: {
          title: 'Преглед на доклад от препоръки',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteRecommendation: 'Изтрий'
        },
        organizationDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса',
          file: 'Файл'
        },
        newOrganizationDocApplication: {
          title: 'Ново заявление',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOrganizationDocApplication: {
          title: 'Преглед на заявление',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          deleteApplication: 'Изтрий'
        },
        staffManagementDirective: {
          auditPartRequirement: 'Изискване',
          position: 'Предлагана длъжност',
          person: 'Предложено лице',
          testDate: 'Дата на писмен тест',
          testScore: 'Оценка от писмен тест',
          valid: 'Валиден',
          applications: 'Заявления',
          caseType: 'Тип дело'
        },
        organizationDataDirective: {
          name: 'Наименование',
          nameAlt: 'Наим. на поддържащ език',
          code: 'Ид. код',
          uin: 'Булстат',
          cao: 'CAO',
          dateCaoFirstIssue: 'Първо издаване',
          dateCaoLastIssue: 'Последна ревизия',
          dateCaoValidTo: 'САО - дата на валидност',
          ICAO: 'ICAO',
          IATA: 'IATA',
          SITA: 'SITA',
          organizationType: 'Тип организация',
          organizationKind: 'Вид организация',
          phones: 'Телефони',
          webSite: 'Web сайт',
          notes: 'Бележки',
          valid: 'Валидност',
          dateValidTo: 'Валидност до',
          docRoom: 'Документ. е в стая',
          caseTypes: 'Типове дела'
        },
        organizationAddressDirective: {
          newAddress: 'Нов aдрес',
          addressType: 'Тип',
          valid: 'Валидно',
          settlement: 'Населено място',
          address: 'Адрес',
          addressAlt: ' Адрес на поддържащ език',
          phone: 'Телефон(и) за връзка на този адрес',
          fax: 'Факс',
          postalCode: 'Пощенски код',
          contactPerson: 'Лице за контакти',
          email: 'E-мейл адрес'
        },
        certificateDirective: {
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Одит',
          organization: 'Организация',
          airport: 'Летище',
          inspector: 'Проверил',
          valid: 'Валиден',
          ext: 'Продължение',
          date: 'Дата на издаване',
          validToDateExt: 'Дата на изтичане',
          activityTypes: 'Дейности',
          includedDocuments: 'Приложени документи',
          approvalDate: 'Дата на одобрение',
          linkedDocument: 'Връзка с документ от документите',
          revokeDate: 'Дата на отнемане',
          revokeinspector: 'Инспектор',
          specs: 'Сецификации',
          revokeTitle: 'Отнемане',
          aircrafts: 'Списък с ВС за опериране',
          aircarrierServices: 'Списък с предлагани услуги',
          documentsTable: {
            includedDocuments: 'Приложени документи',
            chooseDocuments: 'Избери документи',
            inspector: 'Инспектор',
            approvalDate: 'Дата на одобрение',
            linkedDocument: 'Връзка с документ от документите',
            noAvailableDocuments: 'Няма налични приложени документи'
          },
          revokeCause: 'Причина за отнемане'
        },
        equipmentDirective: {
          name: 'Наименование',
          id: 'Инвентарен №',
          count: 'Брой',
          noAvailableEquipments: 'Няма налични съоръжения'
        },
        approvalDirective: {
          approvalType: 'Тип одобрение',
          documentNumber: 'Номер',
          documentDateIssue: 'Дата на издаване',
          approvalState: 'Състояние на одобрението',
          approvalStateDate: 'Дата',
          approvalStateNote: 'Бележки по състоянието',
          recommendationReport: 'Доклад за препоръки',
          caseType: 'Тип дело'
        },
        amendmentDirective: {
          documentNumber: 'Референтен № на описание	',
          documentDateIssue: 'Дата на издаване',
          changeNum: '№ на изменение',
          noAvailableLimitations: 'Няма налични данни',
          lims147: {
            title: 'Обхват на одобрение',
            sortOrder: 'Маркер за сортиране',
            lim147limitation: 'Ограничение по част 147',
            lim147limitationText: 'Ограничения - свободен текст'
          },
          lims145: {
            title: 'Обхват на одобрение',
            basic: 'Базово',
            lim145limitation: 'Ограничение по част MF/145',
            lim145limitationText: 'Ограничения - свободен текст',
            line: 'Линейно'
          },
          limsMG: {
            title: 'Обхват на одобрение',
            typeAC: 'Тип ВС',
            qualitySystem: 'Организация',
            awapproval: 'Разрешен преглед на летателната годност',
            pfapproval: 'Разрешен Permits to Fly'
          },
          includedDocuments: {
            title: 'Приложени документи към одобрение на организация',
            chooseDocuments: 'Избери документи',
            inspector: 'Инспектор',
            approvalDate: 'Дата на одобрение',
            linkedLim: 'Връзка с Обхват на одобрение',
            linkedDocument: 'Връзка с документ от документите на организацията',
            noAvailableDocuments: 'Няма налични документи'
          }
        },
        awExaminerDirective: {
          newStaffManagement: 'Нов проверяващ',
          nomValueId: 'Идентификатор',
          examinerCode: 'Идентификационен код',
          name: 'Наименование',
          nameAlt: 'Наименование на поддържащ език',
          alias: 'Псевдоним',
          permitedAW: 'Разрешена проверка на ЛГ',
          permitedCheck: 'Разрешена проверка на лица',
          valid: 'Валиден',
          person: 'Физическо лице',
          content: {
            title: 'Допълнителни данни',
            stampNumber: 'Хартиен №',
            organization: 'Организация',
            permitedAW: 'Разрешена проверка на ЛГ',
            permitedCheck: 'Разрешена проверка на лица'
          },
          approvedAircrafts: {
            title: 'Одобрени ВС за преглед на ЛГ',
            aircraftTypeGroup: 'Тип/група ВС',
            dateApproved: 'Дата на одобрение',
            inspector: 'Одобрено от инспектор',
            valid: 'Валидно',
            notes: 'Бележки',
            noEntry: 'Няма налични данни'
          },
          applications: 'Заявления'
        },
        recommendationDirective: {
          auditPart: 'Тип',
          part1Title: '1. Общи положения',
          form: 'Форма за заявен обхват на одобрението',
          formDate: 'от дата',
          formText: '№ на изменението',
          interviewedStaff: 'Интервюиран персонал',
          inspectionPeriod: 'Период на надзора',
          inspectionFromDate: 'от',
          inspectionToDate: 'до',
          finishedDate: 'Дата на прикл.',
          town: 'Отдел ЛГ',
          addInspector: 'Добави инспектори',
          inspector: 'Инспектор',
          inspectors: 'Инспектори',
          noAvailableInspectors: 'Няма добавени инспектори',
          part2Title: '2. Одиторски преглед за съответствие',
          audits: 'Одити към доклада от препоръки',
          chooseAudits: 'Избор на одити към доклада от препоръки',
          inspectionStartDate: 'Начална дата',
          inspectionEndDate: 'Крайна дата',
          inspectionSubject: 'Предмет на одит',
          noAvailableAudits: 'Няма добавени одити',
          part3Title: '3. Съответсвие с описанието',
          documentDescription: 'Описание, издание, ревизия',
          recommendationDetailsTitle: 'Обобщени констатации',
          insertRecommendationDetails: 'Въведи списъка за обобщени констатации',
          detailCode: 'Код',
          detailSubject: 'Тема',
          detailAuditResult: 'Констатация',
          detailDisparities: 'Несъответствия',
          part4Title: '4. Установени несъответсвия',
          inspectionsDisparitiesTitle: 'Установени несъответствия от одиторски преглед',
          disparitiesTitle: 'Установени несъответствия от описанието',
          disparitiesIndex: '№',
          disparitiesRefNumber: 'Реф. №',
          disparitiesDescription: 'Описание на несъответствие',
          disparitiesDisparityLevel: 'Ниво',
          disparitiesRemovalDate: 'Дата за отстраняване',
          disparitiesClosureDate: 'Дата на закриване',
          disparitiesRectifyAction: 'Внесени коригиращи действия',
          disparitiesClosureDocument: '№ на документ за закриване',
          noAvailableDisparities: 'Няма добавени несъответствия',
          part5Title: '5. Препоръки',
          recommendation: 'Препоръки'
        },
        organizationRegisterDirective: {
          certNumber: '№ на удостоверение',
          airport: 'Летище',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          organization: 'Организация',
          address: 'Седалище и адрес на управление на оператора',
          activityTypes: 'Дейности',
          revokeTitle: 'Отнемане',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
        }
      },
      errorTexts: {
        required: 'Задължително поле',
        min: 'Стойността на полето трябва да е по-голяма от 0',
        mail: 'Невалиден E-mail',
        uin : 'Невалидно ЕГН',
        lin: 'Невалиден ЛИН',
        date: 'Невалидна дата',
        noFile: 'Моля, прикачете файл',
        noPDForIMGFile: 'Моля, прикачете PDF или IMAGE файл',
        failedRecognition: 'Грешка в разпознаването на файла',
        notUniqueUin: 'Въведеното ЕГН вече съществува в системата',
        invalidNotes: 'Бележки трябва да бъдат попълнени',
        invalidNotesAlt: 'Бележки лат. трябва да бъдат попълнени',
        notUniqueDocData: 'Дублиран документ по основни данни',
        notValidRating: 'Вече съществува такава квалификация',
        notUniqueLicenceNumber: 'Съществува лиценз от същия вид със същия номер',
        notUniqueMSN: 'Серийния номер не е уникален'
      },
      defaultErrorTexts: {
        required: 'Задължително поле',
        pattern: 'Невалиден формат',
        minlength: 'Нарушена минимална дължина на полето',
        maxlength: 'Нарушена максимална дължина на полето',
        min: 'Нарушена минимална стойност на полето',
        max: 'Нарушена максимална стойност на полето',
        unique: 'Полето не е уникално'
      },
      successTexts: {
        successExtract: 'Успешно разпознаване'
      },
      statusTexts: {
        noSuchLastLicenceNumber: 'Не е издаван такъв лиценз'
      },
      states: {
        'root.applications': 'Заявления',
        'root.applications.new': 'Ново заявление',
        'root.applications.new.editApp': 'Преглед',
        'root.applications.edit': 'Преглед',
        'root.applications.edit.data': 'Данни на заявление',
        'root.applications.edit.case': 'Преписка',
        'root.applications.edit.quals': 'Квалификации',
        'root.applications.edit.licenses': 'Лицензи',
        'root.applications.edit.case.newFile': 'Нов документ',
        'root.applications.edit.case.addPart': 'Добавяне',
        'root.applications.edit.case.linkPart': 'Свързване',
        'root.applications.edit.stages': 'Дейности по заявление',
        'root.persons': 'Физически лица',
        'root.persons.stampedDocuments': 'Печат на доументи',
        'root.persons.new': 'Ново физическо лице',
        'root.persons.securityExam': 'Нов теоретичен изпит АС',
        'root.persons.view': 'Лично досие',
        'root.persons.view.edit': 'Преглед на лични данни',
        'root.persons.view.addresses': 'Адреси',
        'root.persons.view.addresses.new': 'Нов адрес',
        'root.persons.view.addresses.edit': 'Преглед на адрес',
        'root.persons.view.statuses': 'Състояния',
        'root.persons.view.statuses.new': 'Ново състояние',
        'root.persons.view.statuses.edit': 'Преглед на състояние',
        'root.persons.view.documentIds': 'Документи за самоличност',
        'root.persons.view.documentIds.new': 'Нов документ за самоличност',
        'root.persons.view.documentIds.edit': 'Преглед на документ за самоличност',
        'root.persons.view.documentEducations': 'Образования',
        'root.persons.view.documentEducations.new': 'Ново образование',
        'root.persons.view.documentEducations.edit': 'Преглед на образование',
        'root.persons.view.licences': 'Лицензи',
        'root.persons.view.licences.new': 'Нов лиценз',
        'root.persons.view.licences.view.editions.new': 'Ново вписване на лиценз',
        'root.persons.view.licences.view.editions.edit': 'Преглед на лиценз',
        'root.persons.view.checks': 'Проверки',
        'root.persons.view.checks.new': 'Нова проверка',
        'root.persons.view.checks.edit': 'Преглед на проверка',
        'root.persons.view.employments': 'Месторабота',
        'root.persons.view.employments.new': 'Новa месторабота',
        'root.persons.view.employments.edit': 'Преглед на месторабота',
        'root.persons.view.medicals': 'Медицински',
        'root.persons.view.medicals.new': 'Новo медицинско',
        'root.persons.view.medicals.edit': 'Преглед на медицинско',
        'root.persons.view.documentTrainings': 'Обучения',
        'root.persons.view.documentTrainings.new': 'Ново обучение',
        'root.persons.view.documentTrainings.edit': 'Преглед на обучение',
        'root.persons.view.documentLangCerts': 'Свидетелства за език',
        'root.persons.view.documentLangCerts.new': 'Ново свидетелство',
        'root.persons.view.documentLangCerts.edit': 'Преглед на свидетелство',
        'root.persons.view.flyingExperiences': 'Летателен / практически опит',
        'root.persons.view.flyingExperiences.new': 'Нов летателен / практически опит',
        'root.persons.view.flyingExperiences.edit': 'Преглед на летателен / практически опит',
        'root.persons.view.ratings': 'Квалификации',
        'root.persons.view.ratings.new': 'Нов квалификационен клас',
        'root.persons.view.ratings.edit.editions.new': 'Ново вписване на квалификация',
        'root.persons.view.ratings.edit.editions.edit': 'Преглед на квалификационен клас',
        'root.persons.view.examASs': 'Теоритични изпити АС',
        'root.persons.view.examASs.new': 'Нов теоритичен изпит АС',
        'root.persons.view.examASs.edit': 'Преглед на теоритичен изпит АС',
        'root.persons.view.inventory': 'Опис',
        'root.persons.view.documentOthers': 'Други документи',
        'root.persons.view.documentOthers.new': 'Нов документ',
        'root.persons.view.documentOthers.edit': 'Преглед на документ',
        'root.persons.view.documentApplications': 'Заявления',
        'root.persons.view.reports': 'Отчети',
        'root.persons.view.reports.new': 'Нов отчет',
        'root.persons.view.reports.edit': 'Преглед на отчет',
        'root.persons.view.examinationSystem': 'Изпитна система',
        'root.aircrafts': 'ВС',
        'root.aircrafts.registrations': 'Регистрации на Въздухоплавателни средства',
        'root.aircrafts.invalidActNumbers': 'Невалидни деловодни номера',
        'root.aircrafts.new': 'Ново ВС',
        'root.aircrafts.view': 'Данни за ВС',
        'root.aircrafts.view.edit': 'Преглед',
        'root.aircrafts.view.regsFM': 'Регистрации',
        'root.aircrafts.view.regsFM.new': 'Нова регистрация',
        'root.aircrafts.view.regsFM.edit': 'Преглед на регистрация',
        'root.aircrafts.view.currentReg': 'Последна регистрация',
        'root.aircrafts.view.smods': 'S-code',
        'root.aircrafts.view.airworthinessesFM': 'Летателни годности',
        'root.aircrafts.view.airworthinessesFM.new': 'Нова годност',
        'root.aircrafts.view.airworthinessesFM.edit': 'Преглед на годност',
        'root.aircrafts.view.noises': 'Удостоверения за шум',
        'root.aircrafts.view.noises.new': 'Ново удостоверение',
        'root.aircrafts.view.noises.edit': 'Преглед на удостоверение',
        'root.aircrafts.view.radiosFM': 'Разрешителни за използване на радиостанция',
        'root.aircrafts.view.radios': 'Разрешителни за използване на радиостанция',
        'root.aircrafts.view.radios.new': 'Ново разрешително',
        'root.aircrafts.view.radios.edit': 'Преглед на разрешително',
        'root.aircrafts.view.debtsFM': 'Тежести',
        'root.aircrafts.view.debtsFM.new': 'Нова тежест',
        'root.aircrafts.view.debtsFM.edit': 'Преглед на тежест',
        'root.aircrafts.view.others': 'Други документи',
        'root.aircrafts.view.others.new': 'Нов документ',
        'root.aircrafts.view.others.edit': 'Преглед на документ',
        'root.aircrafts.view.owners': 'Свързани лица',
        'root.aircrafts.view.owners.new': 'Ново свързано лице',
        'root.aircrafts.view.owners.edit': 'Преглед на свързано лице',
        'root.aircrafts.view.inspections': 'Инспекции',
        'root.aircrafts.view.inspections.new': 'Нова инспекция',
        'root.aircrafts.view.inspections.edit': 'Преглед на инспекция',
        'root.aircrafts.view.occurrences': 'Инциденти',
        'root.aircrafts.view.occurrences.new': 'Нов инцидент',
        'root.aircrafts.view.occurrences.edit': 'Преглед на инцидент',
        'root.aircrafts.view.applications': 'Заявления',
        'root.aircrafts.view.inventory': 'Опис',
        'root.organizations': 'Организации',
        'root.organizations.new': 'Нова организация',
        'root.organizations.view': 'Данни за организация',
        'root.organizations.view.edit': 'Преглед',
        'root.organizations.view.addresses': 'Адреси',
        'root.organizations.view.addresses.new': 'Нов адрес',
        'root.organizations.view.addresses.edit': 'Преглед на адрес',
        'root.organizations.view.certAirportOperators': 'Лицензи на летищен оператор',
        'root.organizations.view.certAirportOperators.new': 'Нов лиценз',
        'root.organizations.view.certAirportOperators.edit': 'Преглед на лиценз',
        'root.organizations.view.certAirOperators': 'Свидетелство за авиационен оператор',
        'root.organizations.view.certAirOperators.new': 'Ново cвидетелство',
        'root.organizations.view.certAirOperators.edit': 'Преглед на свидетелство',
        'root.organizations.view.certAirCarriers': 'Оперативен лиценз на въздушен превозвач',
        'root.organizations.view.certAirCarriers.new': 'Нов лиценз',
        'root.organizations.view.certAirCarriers.edit': 'Преглед на лиценз',
        'root.organizations.view.certAirNavigationServiceDeliverers':
          'Свидетелство за извършване на аеронавигационно обслужване',
        'root.organizations.view.certAirNavigationServiceDeliverers.new': 'Ново свидетелство',
        'root.organizations.view.certAirNavigationServiceDeliverers.edit':
          'Преглед на свидетелство',
        'root.organizations.view.documentOthers': 'Други документи',
        'root.organizations.view.documentOthers.new': 'Нов документ',
        'root.organizations.view.documentOthers.edit': 'Преглед на документ',
        'root.organizations.view.staffManagement': 'Ръководен персонал',
        'root.organizations.view.staffManagement.new': 'Нов ръководен персонал',
        'root.organizations.view.staffManagement.edit': 'Преглед на ръководен персонал',
        'root.organizations.view.certGroundServiceOperators':
          'Лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.certGroundServiceOperators.new':
          'Нов лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.certGroundServiceOperators.edit':
          'Преглед на лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.groundServiceOperatorsSnoOperational':
          'Удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване',
        'root.organizations.view.groundServiceOperatorsSnoOperational.new':
          'Ново удостоверение',
        'root.organizations.view.groundServiceOperatorsSnoOperational.edit':
          'Преглед на удостоверение',
        'root.organizations.view.recommendations': 'Доклад от препоръки',
        'root.organizations.view.recommendations.new': 'Нов доклад от препоръки',
        'root.organizations.view.recommendations.edit': 'Преглед на доклад от препоръки',
        'root.organizations.view.inspections': 'Одит',
        'root.organizations.view.inspections.new': 'Нов одит',
        'root.organizations.view.inspections.edit': 'Преглед на одит',
        'root.organizations.view.approvals': 'Удостоверение за одобрение',
        'root.organizations.view.approvals.new': 'Ново удостоверение за одобрение',
        'root.organizations.view.approvals.edit': 'Преглед на удостоверение за одобрение',
        'root.organizations.view.amendments': 'Изменения на удостоверение за одобрение',
        'root.organizations.view.amendments.new': 'Ново изменение',
        'root.organizations.view.amendments.edit': 'Преглед на изменение',
        'root.organizations.view.awExaminers': 'Проверяващи',
        'root.organizations.view.awExaminers.new': 'Нов проверяващ',
        'root.organizations.view.awExaminers.edit': 'Преглед на проверяващ',
        'root.organizations.view.documentApplications': 'Заявления',
        'root.airports': 'Летища',
        'root.airports.new': 'Ново летище',
        'root.airports.view': 'Данни за летище',
        'root.airports.view.edit': 'Преглед',
        'root.airports.view.others': 'Други документи',
        'root.airports.view.others.new': 'Нов документ',
        'root.airports.view.others.edit': 'Преглед на документ',
        'root.airports.view.owners': 'Свързани лица',
        'root.airports.view.owners.new': 'Ново свързано лице',
        'root.airports.view.owners.edit': 'Преглед на свързано лице',
        'root.airports.view.opers': 'Удостоверения за експлоатационна годност',
        'root.airports.view.opers.new': 'Ново удостоверение',
        'root.airports.view.opers.edit': 'Преглед на удостоверение',
        'root.airports.view.applications': 'Заявления',
        'root.airports.view.inspections': 'Инспекции',
        'root.airports.view.inspections.new': 'Нова инспекция',
        'root.airports.view.inspections.edit': 'Преглед на инспекция',
        'root.airports.view.inventory': 'Опис',
        'root.equipments': 'Съоръжения',
        'root.equipments.new': 'Ново съоръжение',
        'root.equipments.view': 'Данни за съоръжение',
        'root.equipments.view.edit': 'Преглед',
        'root.equipments.view.others': 'Други документи',
        'root.equipments.view.others.new': 'Нов документ',
        'root.equipments.view.others.edit': 'Преглед на документ',
        'root.equipments.view.owners': 'Свързани лица',
        'root.equipments.view.owners.new': 'Ново свързано лице',
        'root.equipments.view.owners.edit': 'Преглед на свързано лице',
        'root.equipments.view.opers': 'Удостоверения за експлоатационна годност',
        'root.equipments.view.opers.new': 'Ново удостоверение',
        'root.equipments.view.opers.edit': 'Преглед на удостоверение',
        'root.equipments.view.applications': 'Заявления',
        'root.equipments.view.inspections': 'Инспекции',
        'root.equipments.view.inspections.new': 'Нова инспекция',
        'root.equipments.view.inspections.edit': 'Преглед на инспекция',
        'root.equipments.view.inventory': 'Опис',
        'root.printableDocs': 'Документи за печат',
        'root.export': 'Експорт XML',
        'root.export.personsData': 'Данни за Физически лица',
        'root.export.examsData': 'Данни за изпити',
        'root.examinationSystem': 'Изпитна система',
        'root.examinationSystem.qualifications': 'Квалификации',
        'root.examinationSystem.exams': 'Тестове',
        'root.examinationSystem.certCampaigns': 'Сертификационни кампании',
        'root.examinationSystem.certPaths': 'Сертификационни пътища',
        'root.examinationSystem.examinees': 'Изпити',
        'root.personsReports': 'Справки в Персонал',
        'root.personsReports.documents': 'Документи',
        'root.personsReports.licences': 'Лицензи',
        'root.personsReports.ratings': 'Квалификационни класове',
        'root.sModeCodes': 'S-mode кодове',
        'root.sModeCodes.new': 'Нов S-mode код',
        'root.sModeCodes.edit': 'Редакция на S-mode код'
      }
    });
  }]);
}(angular));
