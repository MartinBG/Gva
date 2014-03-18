/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      aircrafts: {
        search: {
          manSN: 'Сериен номер',
          model: 'Модел',
          outputDate: 'Дата на производство',
          icao: 'ICAO код',
          aircraftCategory: 'Тип ВС',
          aircraftProducer: 'Производител',
          engine: 'Двигател',
          propeller: 'Витло',
          ModifOrWingColor: 'Модификация/Цвят на крило',
          'new': 'Ново ВС',
          search: 'Търси'
        },
        aircraftDataDirective: {
          title: 'Данни за ВС',
          manSN: 'Сериен номер',
          model: 'Модел',
          modelAlt: 'Модел (английски)',
          outputDate: 'Дата на производство',
          icao: 'ICAO код',
          aircraftCategory: 'Тип ВС',
          aircraftProducer: 'Производител',
          engine: 'Двигател',
          engineAlt: 'Двигател (английски)',
          propeller: 'Витло',
          propellerAlt: 'Витло (английски)',
          ModifOrWingColor: 'Модификация/Цвят на крило',
          ModifOrWingColorAlt: 'Модификация/Цвят на крило (английски)',
          docRoom: 'Документацията е в стая',
          cofAType: 'CofA Type',
          tcds: 'TCDS',
          easaType: 'EASA Type',
          easaCategory: 'EASA Категория',
          euRegType: 'EASA Reg',
          maxMassL: 'Макс. маса при излитане',
          maxMassT: 'Макс. маса при кацане/Полезен товар',
          seats: 'Брой места'
        },
        newAircraft: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        viewAircraft: {
          aircraftProducer: 'Производител',
          aircraftCategory: 'Тип ВС',
          icao: 'ICAO',
          model: 'Модел ВС',
          modelAlt: 'Модел ВС (английски)',
          manSN: 'MSN (сериен номер)',
          cofAType: 'CofA Type',
          edit: 'Редакция'
        },
        regSearch: {
          isActive: 'Активна',
          certNumber: '№',
          certDate: 'Дата на издаване',
          aircraftTypeCertificateType: 'Типов сертификат',
          aircraftNewOld: 'Ново ВС',
          operationType: 'Тип на опериране',
          removalDate: 'Дата на отписване',
          removalReason: 'Причина за отписване',
          newReg: 'Нова регистрация'
        },
        newReg: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editReg: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        regView: {
          first: 'Първа рег.',
          last: 'Последна рег.',
          prev: 'Предходна рег.',
          next: 'Следваща рег.',
          firstReg: 'Първа рег № ',
          lastReg: 'Посл. рег. № ',
          register: 'рег. ',
          regFrom: ' от '
        },
        smodSearch: {
          valid: 'Валиден',
          scode: 'S-mode code',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата',
          newSmod: 'Нов S-code'
        },
        newSmod: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editSmod: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        markSearch: {
          valid: 'Валиден',
          mark: 'Рег. знак',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата',
          newMark: 'Нов знак'
        },
        newMark: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editMark: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        airworthinessSearch: {
          aircraftCertificateType: 'Валиден',
          refNumber: 'Рег. знак',
          issueDate: 'Тяхно писмо №',
          validToDate: 'Тяхна дата',
          newAirworthiness: 'Нова годност'
        },
        newAirworthiness: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAirworthiness: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        permitSearch: {
          issuePlace: 'Място на издаване',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          pointFrom: 'Начален пункт',
          pointTo: 'Краен пункт',
          planStops: 'Планирани спирания',
          crew: 'Екипаж',
          newPermit: 'Ново разрешение'
        },
        newPermit: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editPermit: {
          save: 'Запис',
          cancel: 'Отказ'
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
        noiseSearchFM: {
          standart: 'Стандарт',
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
          save: 'Запис',
          cancel: 'Отказ'
        },
        editNoise: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        radioSearch: {
          valid: 'Валидно',
          certNumber: '№',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          aircraftRadioType: 'Тип на радиооборудването',
          count: 'Брой',
          producer: 'Производител',
          model: 'Модел',
          newRadio: 'Ново разрешително'
        },
        newRadio: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRadio: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        debtSearchFM: {
          certId: 'Регистрация №',
          regDate: 'Дата',
          aircraftDebtType: 'Ипотека/Запор',
          documentNumber: 'Вх.док ГВА',
          documentDate: 'Дата на док',
          aircraftCreditor: 'Кредитор',
          inspector: 'Инспектор',
          newDebt: 'Нов залог'
        },
        newDebtFM: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDebtFM: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        debtSearch: {
          certId: 'Регистрация №',
          regDate: 'Дата',
          aircraftDebtType: 'Ипотека/Запор',
          contractNumber: 'Вх.док ГВА',
          contractDate: 'Дата на док',
          creditorName: 'Кредитор',
          inspector: 'Инспектор',
          newDebt: 'Нов залог'
        },
        newDebt: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editDebt: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        regViewDirective: {
          title: 'Текуща регистрация',
          currentCert: 'Текущ запис',
          lastCert: 'Последен запис',
          firstCert: 'Първи запис',
          certNumber: 'Рег. номер',
          register: 'Регистър',
          regMark: 'Рег. знак',
          certDate: 'Дата на рег.',
          owner: 'Собственик',
          oper: 'Оператор',
          operationType: 'Категория',
          limitations: 'Ограничения',
          aircraftTypeCertificateType: 'Типов сертификат'
        },
        smodViewDirective: {
          title: 'S-code',
          valid: 'Валиден',
          scode: 'S-mode code',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата'
        },
        markViewDirective: {
          title: 'Регистрационен знак',
          valid: 'Валиден',
          mark: 'Рег. знак',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата'
        },
        airworthinessViewDirective: {
          title: 'Летателна годност',
          aircraftCertificateType: 'Тип',
          regNumber: '№',
          refNumber: 'Реф.№',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност'
        },
        noiseViewDirective: {
          title: 'Удостоверение за шум',
          issueNumber: '№',
          tcdsn: 'TCDSN',
          issueDate: 'Дата на издаване',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане'
        },
        permitViewDirective: {
          title: 'Разрешениe за полет',
          issuePlace: 'Място на издаване',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          pointFrom: 'Начален пункт',
          pointTo: 'Краен пункт',
          planStops: 'Планирани спирания',
          crew: 'Екипаж'
        },
        radioViewDirective: {
          title: 'Разрешително за използване на радиостанция',
          valid: 'Валидно',
          certNumber: '№',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          aircraftRadioType: 'Тип на радиооборудването',
          count: 'Брой',
          producer: 'Производител',
          model: 'Модел'
        },
        smodDirective: {
          title: 'S-code',
          valid: 'Валиден',
          scode: 'S-mode code',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата',
          caaTo: 'ГВА писмо до',
          caaJob: 'ГВА писмо длъжност',
          caaToAddress: 'ГВА писмо адрес',
          getScode: 'Генерирай S-код'
        },
        markDirective: {
          title: 'Регистрационен знак',
          valid: 'Валиден',
          mark: 'Регистрационен знак',
          ltrInNumber: 'Тяхно писмо №',
          ltrInDate: 'Тяхна дата',
          ltrCaaNumber: 'ГВА писмо №',
          ltrCaaDate: 'ГВА дата'
        },
        airworthinessDirective: {
          title: 'Летателна годност',
          aircraftCertificateType: 'Тип',
          regNumber: '№',
          refNumber: 'Реф.№',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност'
        },
        permitDirective: {
          title: 'Разрешениe за полет',
          issuePlace: 'Място на издаване',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          purpose: 'Обхват на разрешението',
          notes: 'Условия/Забележки',
          pointFrom: 'Начален пункт',
          pointTo: 'Краен пункт',
          planStops: 'Планирани спирания',
          crew: 'Екипаж',
          purposeAlt: 'Обхват на разрешението (англ.)',
          notesAlt: 'Условия/Забележки (англ.)',
          pointFromAlt: 'Начален пункт (англ.)',
          pointToAlt: 'Краен пункт (англ.)',
          planStopsAlt: 'Планирани спирания (англ.)',
          crewAlt: 'Екипаж (англ.)'
        },
        noiseDirective: {
          title: 'Удостоверение за шум',
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
          notes: 'Забележки'
        },
        noiseDirectiveFM: {
          title: 'Удостоверение за шум',
          issueNumber: '№',
          tcdsn: 'TCDSN',
          chapter: 'Chapter',
          issueDate: 'Дата на издаване',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане',
          modifications: 'Модификации',
          modificationsAlt: 'Модификации (англ.)',
          notes: 'Забележки'
        },
        radioDirective: {
          title: 'Разрешително за използване на радиостанция',
          certNumber: '№',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          valid: 'Валидно',
          aircraftRadioType: 'Тип на радиооборудването',
          count: 'Брой',
          producer: 'Производител',
          model: 'Модел',
          power: 'Мощност',
          'class': 'Клас на излъчване',
          bandwidth: 'Честотна лента'
        },
        regDirective: {
          title: 'Регистрация',
          typeCertTitle: 'Типов сертификат',
          certNumber: 'Рег. номер',
          certDate: 'Дата на издаване',
          register: 'Регистър',
          aircraftCertificateType: 'Вид удостоверение',
          aircraftNewOld: 'ВС е ново',
          operationType: 'Предвиждан тип на опериране',
          inspector: 'Инспектор',
          oper: 'Оператор',
          typeCertNumber: 'Рег. номер',
          typeCertDate: 'Дата на издаване',
          typeCertRelease: 'Издание номер',
          contry: 'Държава',
          aircraftTypeCertificateType: 'Типов сертификат'
        },
        debtDirectiveFM: {
          title: 'Залог/запор',
          certId: 'Регистрация №',
          regDate: 'Дата',
          regTime: 'Час',
          aircraftDebtType: 'Ипотека/Запор',
          documentNumber: 'Вх.док ГВА',
          documentDate: 'Дата на док',
          aircraftCreditor: 'Кредитор',
          creditorDocument: 'Док. и дата писма на кредитор',
          inspector: 'Инспектор'
        },
        debtDirective: {
          title: 'Залог/запор',
          certId: 'Регистрация №',
          regDate: 'Дата',
          regTime: 'Час',
          aircraftDebtType: 'Ипотека/Запор',
          contractNumber: 'Вх.док ГВА',
          contractDate: 'Дата на док',
          inspector: 'Инспектор',
          creditorName: 'Кредитор',
          creditorNameAlt: 'Кредитор (англ.)',
          creditorData: 'Данни за кредитора',
          creditorAddress: 'Адрес',
          creditorEmail: 'Е-майл',
          creditorContact: 'Лице за контакт',
          creditorPhone: 'Телефон',
          startDate: 'Дата на вписване',
          startReason: 'Основание за вписване',
          startReasonAlt: 'Основание за вписване (англ.)',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
          ltrNumber: 'Писмо на ГВА - номер',
          ltrDate: 'Дата',
          endDate: 'Дата на заличаване',
          endReason: 'Основание за заличаване',
          closeInspector: 'Инспектор',
          closeAplicationNumber: 'Писмо за заличаване - номер',
          closeAplicationDate: 'Дата',
          closeCaaAplicationNumber: 'Писмо на ГВА за заличаване - номер',
          closeCaaAplicationDate: 'Дата'
        },
        aircraftOtherDirective: {
          title: 'Друг документ',
          documentNumber: 'Док No',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          otherDocumentType: 'Тип документ',
          aircraftOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.'
        },
        newOther: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOther: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        otherSearch: {
          newDocument: 'Нов документ',
          documentNumber: 'Документ №',
          documentDateValidFrom: 'От дата',
          documentDateValidTo: 'До дата',
          documentPublisher: 'Издател',
          otherDocumentType: 'Тип документ',
          aircraftOtherDocumentRole: 'Роля',
          valid: 'Валидно',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ'
        },
        inspectionDirective: {
          title: 'Инспекция',
          documentNumber: '№ на документ',
          auditState: 'Състояние',
          auditReason: 'Причина',
          auditType: 'Вид одит',
          subject: 'Предмет на одит',
          notification: 'Оператора е предварително уведомен',
          startDate: 'Начална дата',
          endDate: 'Крайна дата',
          inspectionPlace: 'Адрес на одитирания обект',
          inspectionFrom: 'Начална дата на периода, в който ВС може да бъде видяно',
          inspectionTo: 'Крайна дата на периода, в който ВС може да бъде видяно',
          auditAddress: 'Адрес на одитирания обект',
          insertAscertainments: 'Въведи списъка за обобщени констатации',
          ascertainmentsTable: {
            title: 'Главни обобщени констатации',
            subject: 'Тема',
            conclusion: 'Констатация',
            disparity: 'Несъответствия',
            code: 'Код'
          },
          disparitiesTable: {
            subject: 'Тема',
            disparitiesTitle: 'Несъответствия',
            sortOrder: 'Пореден №',
            refNumber: 'Референтен №',
            description: 'Описание на несъответствие',
            disparityLevel: 'Ниво',
            removalDate: 'Дата за отстраняване',
            rectifyAction: 'Внесени коригиращи действия',
            closureDate: 'Дата на закриване',
            closureDocument: '№ на документ за закриване',
            noAvailableDisparities: 'Няма налични несъответствия'
          },
          examinersTable: {
            examinersTitle: 'Одитори',
            checker: 'Одитор',
            sortOrder: 'Пореден №',
            noAvailableExaminers: 'Няма налични одитори'
          }
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
          inspectionPlace: 'Адрес на одитирания обект'
        },
        newInspection: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editInspection: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        occurrenceSearch: {
          localTime: 'Час на инцидента',
          newOccurrence: 'Нов инцидент',
          localDate: 'Дата на инцидента',
          aircraftOccurrenceClass: 'Клас',
          country: 'Държава',
          area: 'Място на инцидента',
          occurrenceNotes: 'Бележки по инцидента',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в деловодна книга',
          pageCount: 'Бр. страници на документа'
        },
        newOccurrence: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOccurrence: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        occurrenceDirective: {
          title: 'Инцидент',
          localDate: 'Дата на инцидента',
          localTime: 'Час на инцидента',
          aircraftOccurrenceClass: 'Клас',
          country: 'Държава',
          area: 'Място на инцидента',
          occurrenceNotes: 'Бележки по инцидента',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в деловодна книга',
          pageCount: 'Бр. страници на документа'
        },
        newMaintenance: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editMaintenance: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        searchMaintenance: {
          newMaintenance: 'Нова поддръжка',
          lim145limitation: 'Дейност по част M/F, 145',
          notes: 'Описание',
          fromDate: 'Период: от дата',
          toDate: 'Период: до дата',
          organization: 'Организация',
          person: 'Физическо лице'
        },
        maintenanceDirective: {
          title: 'Поддръжка',
          lim145limitation: 'Дейност по част M/F, 145',
          notes: 'Описание',
          fromDate: 'Период: от дата',
          toDate: 'Период: до дата',
          organization: 'Организация',
          person: 'Физическо лице'
        }
      },
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
        ratingEditionDirective: {
          title: 'Вписване/Потвърждение',
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
        personOtherDirective: {
          title: 'Друг документ',
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.'
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
        otherSearch: {
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          personOtherDocumentType: 'Тип документ',
          personOtherDocumentRole: 'Роля',
          valid: 'Действителен',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ'
        },
        newOther: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOther: {
          save: 'Запис',
          cancel: 'Отказ'
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
          newRating: 'Нов клас',
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
          newEdition: 'Ново вписване/Потвърждаване',
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
        },
        publishers: {
          text: 'Текст',
          publisherType: 'Тип',
          name: 'Наименование',
          back: 'Назад',
          search: 'Търси',
          select: 'Избор'
        }
      },
      applications: {
        edit: {
          personName: 'Име',
          personLin: 'ЛИН',
          status: 'Статус',
          docTypeName: 'Относно',
          editPerson: 'Редакция',
          'case': {
            regNumber: 'Тип/№/Дата',
            description: 'Тип',
            act: 'Дело',
            viewDoc: 'Преглед',
            page: 'стр.',
            linkNew: 'Добави към дело',
            linkPart: 'Свържи с вече добавен',
            newFile: 'Нов документ'
          },
          newFile: {
            title: 'Нов документ в описа',
            documentType: 'Тип на документ',
            cancel: 'Назад',
            addPart: 'Продължи'
          },
          linkFile: {
            title: 'Свържи документ в описа',
            documentType: 'Тип на документ',
            search: 'Търси',
            cancel: 'Назад',
            select: 'Избор'
          },
          addPart: {
            cancel: 'Назад',
            save: 'Запис'
          }
        },
        newForm: {
          person: 'Заявител',
          newPerson: 'Нов заявител',
          selectPerson: 'Избери заявител',
          register: 'Регистрирай',
          cancel: 'Отказ'
        },
        link: {
          person: 'Заявител',
          selectDoc: 'Избор на документ',
          cancel: 'Отказ',
          clear: 'Изчисти',
          document: 'Документ',
          docNumber: 'Рег.№',
          docStatus: 'Статус',
          docName: 'Име',
          link: 'Свържи'
        },
        personSelect: {
          select: 'Избери',
          cancel: 'Отказ',
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
        personNew: {
          saveAndSelect: 'Запис и избор',
          cancel: 'Отказ'
        },
        docSelect: {
          fromDate: 'От дата',
          toDate: 'До дата',
          docName: 'Относно',
          docTypeId: 'Вид на документа',
          docStatusId: 'Статус на документа',
          corrs: 'Кореспонденти',
          units: 'Отнесено към',
          newDoc: 'Нов документ',
          regDate: 'Дата',
          regUri: 'Рег.№',
          docSubject: 'Относно',
          docDirectionName: '',
          docStatusName: 'Статус',
          correspondentName: 'Кореспондент',
          search: 'Търси',
          cancel: 'Отказ',
          select: 'Избери'
        },
        search: {
          fromDate: 'От дата',
          toDate: 'До дата',
          search: 'Търси',
          newApp: 'Ново',
          linkApp: 'Свържи',
          regDate: 'Дата',
          regUri: 'Рег.№',
          subject: 'Относно',
          status: 'Статус',
          lin: 'ЛИН',
          applicant: 'Заявител'
        }
      },
      errorTexts: {
        required: 'Задължително поле',
        min: 'Стойността на полето трябва да е по-голяма от 0',
        mail: 'Невалиден E-mail',
        lin: 'Невалиден ЛИН'
      },
      states: {
        'root.applications': 'Заявления',
        'root.applications.new': 'Ново заявление',
        'root.applications.new.personSelect': 'Избер на заявител',
        'root.applications.new.personNew': 'Нов заявител',
        'root.applications.link': 'Свържи заявление',
        'root.applications.link.docSelect': 'Избор на документ',
        'root.applications.link.personSelect': 'Избер на заявител',
        'root.applications.link.personNew': 'Нов заявител',
        'root.applications.edit': 'Редакция',
        'root.applications.edit.case': 'Преписка',
        'root.applications.edit.quals': 'Квалификации',
        'root.applications.edit.licenses': 'Лицензи',
        'root.applications.edit.newFile': 'Нов документ',
        'root.applications.edit.addPart': 'Добавяне',
        'root.applications.edit.linkPart': 'Свързване',
        'root.persons': 'Физически лица',
        'root.persons.new': 'Ново физическо лице',
        'root.persons.view': 'Лично досие',
        'root.persons.view.edit': 'Редакция',
        'root.persons.view.addresses': 'Адреси',
        'root.persons.view.addresses.new': 'Нов адрес',
        'root.persons.view.addresses.edit': 'Редакция на адрес',
        'root.persons.view.statuses': 'Състояния',
        'root.persons.view.statuses.new': 'Ново състояние',
        'root.persons.view.statuses.edit': 'Редакция на състояние',
        'root.persons.view.documentIds': 'Документи за самоличност',
        'root.persons.view.documentIds.new': 'Нов документ за самоличност',
        'root.persons.view.documentIds.edit': 'Редакция на документ за самоличност',
        'root.persons.view.documentEducations': 'Образования',
        'root.persons.view.documentEducations.new': 'Ново образование',
        'root.persons.view.documentEducations.edit': 'Редакция на образование',
        'root.persons.view.licences': 'Лицензи',
        'root.persons.view.checks': 'Проверки',
        'root.persons.view.checks.new': 'Нова проверка',
        'root.persons.view.checks.new.choosePublisher': 'Избор на издател',
        'root.persons.view.checks.edit': 'Редакция на проверка',
        'root.persons.view.checks.edit.choosePublisher': 'Избор на издател',
        'root.persons.view.employments': 'Месторабота',
        'root.persons.view.employments.new': 'Новa месторабота',
        'root.persons.view.employments.edit': 'Редакция на месторабота',
        'root.persons.view.medicals': 'Медицински',
        'root.persons.view.medicals.new': 'Новo медицинско',
        'root.persons.view.medicals.edit': 'Редакция на медицинско',
        'root.persons.view.documentTrainings': 'Обучение',
        'root.persons.view.documentTrainings.new': 'Ново обучение',
        'root.persons.view.documentTrainings.new.choosePublisher': 'Избор на издател',
        'root.persons.view.documentTrainings.edit': 'Редакция на обучение',
        'root.persons.view.documentTrainings.edit.choosePublisher': 'Избор на издател',
        'root.persons.view.flyingExperiences': 'Летателен / практически опит',
        'root.persons.view.flyingExperiences.new': 'Нов летателен / практически опит',
        'root.persons.view.flyingExperiences.edit': 'Редакция на летателен / практически опит',
        'root.persons.view.ratings': 'Класове',
        'root.persons.view.ratings.new': 'Нов клас',
        'root.persons.view.editions': 'Вписвания / Потвърждения',
        'root.persons.view.editions.new': 'Ново вписване / потвърждение',
        'root.persons.view.editions.edit': 'Редакция на вписване / потвърждение',
        'root.persons.view.inventory': 'Опис',
        'root.persons.view.documentOthers': 'Други документи',
        'root.persons.view.documentOthers.new': 'Нов документ',
        'root.persons.view.documentOthers.new.choosePublisher': 'Избор на издател',
        'root.persons.view.documentOthers.edit': 'Редакция на документ',
        'root.persons.view.documentOthers.edit.choosePublisher': 'Избор на издател',
        'root.aircrafts': 'ВС',
        'root.aircrafts.new': 'Ново ВС',
        'root.aircrafts.view': 'Данни за ВС',
        'root.aircrafts.view.regs': 'Регистрации',
        'root.aircrafts.view.regs.new': 'Нова регистрация',
        'root.aircrafts.view.regs.edit': 'Редакция на регистрация',
        'root.aircrafts.view.currentReg': 'Текуща регистрация',
        'root.aircrafts.view.smods': 'S-code',
        'root.aircrafts.view.smods.new': 'Нов S-code',
        'root.aircrafts.view.smods.edit': 'Редакция на S-code',
        'root.aircrafts.view.marks': 'Регистрационни знакове',
        'root.aircrafts.view.marks.new': 'Нов знак',
        'root.aircrafts.view.marks.edit': 'Редакция на знак',
        'root.aircrafts.view.airworthinesses': 'Летателни годности',
        'root.aircrafts.view.airworthinesses.new': 'Нова годност',
        'root.aircrafts.view.airworthinesses.edit': 'Редакция на годност',
        'root.aircrafts.view.permits': 'Разрешения за полет',
        'root.aircrafts.view.permits.new': 'Ново разрешение',
        'root.aircrafts.view.permits.edit': 'Редакция на разрешение',
        'root.aircrafts.view.noises': 'Удостоверения за шум',
        'root.aircrafts.view.noises.new': 'Ново удостоверение',
        'root.aircrafts.view.noises.edit': 'Редакция на удостоверение',
        'root.aircrafts.view.radiosFM': 'Разрешителни за използване на радиостанция',
        'root.aircrafts.view.noisesFM.new': 'Ново удостоверение',
        'root.aircrafts.view.noisesFM.edit': 'Редакция на удостоверение',
        'root.aircrafts.view.radios': 'Разрешителни за използване на радиостанция',
        'root.aircrafts.view.radios.new': 'Ново разрешително',
        'root.aircrafts.view.radios.edit': 'Редакция на разрешително',
        'root.aircrafts.view.debts': 'Залози',
        'root.aircrafts.view.debts.new': 'Ново залог',
        'root.aircrafts.view.debts.edit': 'Редакция на залог',
        'root.aircrafts.view.debtsFM': 'Залози',
        'root.aircrafts.view.debtsFM.new': 'Ново залог',
        'root.aircrafts.view.debtsFM.edit': 'Редакция на залог',
        'root.aircrafts.view.others': 'Други документи',
        'root.aircrafts.view.others.new': 'Нов документ',
        'root.aircrafts.view.others.edit': 'Редакция на документ'
      }
    });
  }]);
}(angular));
