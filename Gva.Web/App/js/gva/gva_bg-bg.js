/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      common: {
        publishers: {
          text: 'Текст',
          publisherType: 'Тип',
          name: 'Наименование',
          back: 'Назад',
          search: 'Търси',
          select: 'Избор'
        },
        auditDetailDirective: {
          auditPart: 'Част',
          title: 'Главни обобщени констатации',
          subject: 'Тема',
          auditResult: 'Констатация',
          disparity: 'Несъответствия',
          code: 'Код',
          insertAuditDetails: 'Въведи списъка за обобщени констатации'
        },
        disparityDirective: {
          disparitiesRecommendationsTitle2: 'Установени несъответствия от описанието',
          disparitiesTitle: 'Несъответствия',
          subject: 'Тема',
          sortOrder: 'Пореден №',
          refNumber: 'Референтен №',
          description: 'Описание на несъответствие',
          disparityLevel: 'Ниво',
          removalDate: 'Дата за отстраняване',
          rectifyAction: 'Внесени коригиращи действия',
          closureDate: 'Дата на закриване',
          closureDocument: '№ на документ за закриване',
          noAvailableDisparities: 'Няма намерени резултати',
          auditPart: 'Част'
        },
        inspectionDirective: {
          organizationTitle: 'Одит',
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
          examinersTable: {
            examinersTitle: 'Одитори',
            examiner: 'Одитор',
            sortOrder: 'Пореден №',
            noAvailableExaminers: 'Няма намерени резултати'
          },
          auditPart: 'Част',
          recommendationsReport: 'Доклад от препоръки свързан с одит'
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
          inspections: 'Инспекции'
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
        airportDataDirective: {
          title: 'Данни за летище',
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
          place: 'Местоположение',
          icao: 'ICAO код',
          runway: 'Полоса',
          course: 'Курс',
          excess: 'Превишение ',
          concrete: 'Полоса-бетон',
          edit: 'Редакция'
        },
        newAirport: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        airportOtherDirective: {
          title: 'Друг документ',
          documentNumber: 'Док No',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          otherDocumentType: 'Тип документ',
          airportOtherDocumentRole: 'Роля',
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
        airportOwnerDirective: {
          title: 'Свързано лице',
          airportRelation: 'Тип отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: '№ на документ',
          documentDate: 'Дата на документ',
          fromDate: 'В сила от',
          toDate: 'Дата на прекратявне на отношенията',
          reasonTerminate: 'Причина за прекратяване',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.'
        },
        newOwner: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOwner: {
          save: 'Запис',
          cancel: 'Отказ'
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
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.'
        },
        airportOperDirective: {
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
          revokeCause: 'Причина за отнемане'
        },
        newOper: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOper: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        operSearch: {
          newOper: 'Ново удостоверение',
          issueDate: 'Дата на издаване',
          issueNumber: 'Удостоверение №',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          inspector: 'Проверил',
          valid: 'Валиден'
        },
        airportDocApplicationDirective: {
          title: 'Заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        airportDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        newAirportDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAirportDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      aircrafts: {
        tabs: {
          reg: 'Регистрация',
          currentReg: 'Текуща регистрация',
          regs: 'Регистрации',
          airworthinesses: 'Летателни годности',
          marks: 'Регистрационни знаци',
          smods: 'S-code',
          permits: 'Разрешения за полет',
          noisesFM: 'Удостоверения за шум',
          radios: 'Разрешителни за радиостанция',
          docs: 'Документи',
          parts: 'Оборудване',
          debts: 'Залози',
          maintenances: 'Поддръжки',
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
        aircraftDataApexDirective: {
          title: 'Данни за ВС',
          model: 'Модел',
          name: 'Наименование',
          nameAlt: 'Наименование (английски)',
          series: 'Серия',
          aircraftTypeGroup: 'Група ВС',
          aircraftSCodeType: 'Тип (S-code)',
          manSN: 'Сериен номер',
          manPlace: 'Място на производство',
          manDate: 'Дата на производство',
          beaconCodeELT: 'Beacon код (60 битов) на ELT',
          icao: 'ICAO код',
          aircraftCategory: 'Тип ВС',
          aircraftProducer: 'Производител',
          docRoom: 'Документацията е в стая',
          maxMassL: 'Макс. маса при излитане',
          maxMassT: 'Макс. маса при кацане',
          massData: 'Маса и център на тежестта',
          ultralightData: 'За свръхлеки ВС',
          noiseData: 'Шум',
          radioData: 'Разрешително за радиостанция',
          mass: 'Маса (kg)',
          cax: '% CAX',
          date: 'Дата на протокол от замерване',
          color: 'Цвят на крилото',
          colorAlt: 'Цвят на крилото (английски)',
          payload: 'Полезен товар',
          seats: 'Брой места',
          flyover: 'Прелитане',
          approach: 'Приближаване',
          lateral: 'Странично',
          overflight: 'Полет над',
          takeoff: 'Излитане',
          approvalNumber: 'Номер разрешително',
          approvalDate: 'Дата',
          incommingApprovalNumber: 'Документ - вх.номер',
          incommingApprovalDate: 'Дата'
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
        regFMSearch: {
          isActive: 'Активна',
          certNumber: 'Рег. номер',
          certDate: 'Дата на издаване',
          register: 'Регистър',
          regMark: 'Регистрационен знак',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          incomingDocDesc: 'Други документи или причини',
          inspector: 'Инспектор',
          owner: 'Собственик',
          operator: 'Оператор',
          category: 'Категория',
          limitation: 'Ограничения',
          leasingDocNumber: 'Заповед за лизинг',
          leasingDocDate: 'Дата',
          leasingLessor: 'Лизингодател',
          leasingAgreement: 'Договор за лизинг и анекси към него',
          leasingEndDate: 'Срок',
          status: 'Състояние',
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
          removalDocumentDate: 'Дата на документ за отписване',
          removalInspector: 'Инспектор, отписал ВС',
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
        airworthinessFMSearch: {
          issueDate: 'От дата',
          validFromDate: 'Валидно от',
          validToDate: 'Валидно до',
          inspector: 'Заверил инспектор',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          EASA25IssueDate: 'EASA Form 25',
          EASA24IssueDate: 'EASA Form 24',
          EASA24IssueValidToDate: 'EASA Form 24 Valid',
          EASA15IssueDate: 'Form 15 Issue',
          EASA15IssueValidToDate: 'Form 15 Valid',
          EASA15IssueRefNo: 'EASA Form 15a',
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
        airworthinessFMDirective: {
          title: 'Летателна годност',
          issueDate: 'От дата',
          validFromDate: 'Валидно от',
          validToDate: 'Валидно до',
          inspector: 'Заверил инспектор',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          EASA25IssueDate: 'EASA Form 25',
          EASA24IssueDate: 'EASA Form 24',
          EASA24IssueValidToDate: 'EASA Form 24 Valid',
          EASA15IssueDate: 'Form 15 Issue',
          EASA15IssueValidToDate: 'Form 15 Valid',
          EASA15IssueRefNo: 'EASA Form 15a'
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
        regFMDirective: {
          title: 'Регистрация',
          certNumber: 'Рег. номер',
          certDate: 'Дата на издаване',
          register: 'Регистър',
          regMark: 'Регистрационен знак',
          incomingDocNumber: 'Вх. номер ГВА',
          incomingDocDate: 'Дата',
          incomingDocDesc: 'Други документи или причини',
          inspector: 'Инспектор',
          owner: 'Собственик',
          operator: 'Оператор',
          category: 'Категория',
          limitation: 'Ограничения',
          leasingDocNumber: 'Заповед за лизинг',
          leasingDocDate: 'Дата',
          leasingLessor: 'Лизингодател',
          leasingAgreement: 'Договор за лизинг и анекси към него',
          leasingEndDate: 'Срок',
          status: 'Състояние',
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
          removalDocumentDate: 'Дата на документ за отписване',
          removalInspector: 'Инспектор, отписал ВС'
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
        aircraftOwnerDirective: {
          title: 'Свързано лице',
          aircraftRelation: 'Тип отношение',
          person: 'Физическо лице',
          organization: 'Организация',
          documentNumber: '№ на документ',
          documentDate: 'Дата на документ',
          fromDate: 'В сила от',
          toDate: 'Дата на прекратявне на отношенията',
          reasonTerminate: 'Причина за прекратяване',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.'
        },
        newOwner: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOwner: {
          save: 'Запис',
          cancel: 'Отказ'
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
          bookPageNumber: '№ стр. в делов. книга',
          pageCount: 'Брой стр.',
          newOther: 'Нов документ'
        },
        aircraftPartDirective: {
          title: 'Oборудване',
          aircraftPart: 'Тип',
          partProducer: 'Производител',
          model: 'Модел',
          modelAlt: 'Модел (англ.)',
          sn: 'Сериен №',
          count: 'Брой',
          aircraftPartStatus: 'Ново',
          manDate: 'Дата на производство',
          manPlace: 'Място на производство',
          description: 'Описание на характеристики'
        },
        newPart: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editPart: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        partSearch: {
          newPart: 'Ново оборудване',
          aircraftPart: 'Тип',
          partProducer: 'Производител',
          model: 'Модел',
          modelAlt: 'Модел (англ.)',
          sn: 'Сериен №',
          count: 'Брой',
          aircraftPartStatus: 'Ново',
          manDate: 'Дата на производство',
          manPlace: 'Място на производство',
          description: 'Характеристики'
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
        },
        aircraftDocApplicationDirective: {
          title: 'Заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        aircraftDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        newAircraftDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAircraftDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      persons: {
        tabs: {
          licences: 'Лицензи',
          qualifications: 'Квалификации',
          ratings: 'Класове',
          flyingExperiences: 'Летателен / практически опит',
          documentTrainings: 'Обучение',
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
          applications: 'Заявления'
        },
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
          phones: 'Телефони',
          caseTypes: 'Типове дела'
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
          notes: 'Бележки'
        },
        personScannedDocumentDirective: {
          title: 'Електронен (сканиран) документ',
          fileName: 'Име на файл',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          caseType: 'Тип дело',
          applications: 'Заявления'
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
          title: 'Свидетелство за медицинска годност'
        },
        personEmploymentDirective: {
          title: 'Месторабота',
          hiredate: 'Дата на назначаване',
          valid: 'Валиден',
          organization: 'Организация',
          employmentCategory: 'Категория длъжност',
          country: 'Страна',
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
        personDocApplicationDirective: {
          title: 'Заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса'
        },
        personDocApplicationSearch: {
          newApplication: 'Ново заявление',
          documentNumber: '№ на документ',
          documentDate: 'От дата',
          requestDate: 'Дата на заявител',
          notes: 'Бележки',
          bookPageNumber: '№ стр. в дело',
          pageCount: 'Брой стр.',
          applicationType: 'Тип заявление',
          applicationPaymentType: 'Член',
          currency: 'Парична единица',
          taxAmount: 'Платена такса',
          file: 'Файл'
        },
        newPersonDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editPersonDocApplication: {
          save: 'Запис',
          cancel: 'Отказ'
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
          newOther: 'Нов документ',
          file: 'Файл'
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
        }
      },
      applications: {
        edit: {
          personName: 'Име',
          personLin: 'ЛИН',
          status: 'Статус',
          docTypeName: 'Относно',
          viewPerson: 'Преглед',
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
            newDocFile: 'Нов файл по съществуваща страница'
          },
          newFile: {
            title: 'Нов документ в описа',
            documentType: 'Тип на документ',
            cancel: 'Назад',
            addPart: 'Продължи'
          },
          newDocFile: {
            title: 'Нов файл',
            cancel: 'Назад',
            save: 'Запиши'
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
          fromDate: 'От дата на заявител',
          toDate: 'До дата на заявител',
          search: 'Търси',
          newApp: 'Ново',
          linkApp: 'Свържи',
          requestDate: 'Дата на заявител',
          documentNumber: '№ на документ',
          applicationType: 'Тип заявление',
          status: 'Статус',
          lin: 'ЛИН',
          applicant: 'Заявител'
        },
        applicationDocument: {
          title: 'Сканиран (електронен) документ',
          name: 'Наименование',
          fileKind: 'Вид файл',
          fileType: 'Тип файл',
          caseType: 'Тип дело',
          pageIndex: '№ стр. в дело',
          pageNumber: 'Брой стр.',
          attachment: 'Прикачен файл'
        }
      },
      organizations: {
        tabs: {
          approvals: 'Удостоверения за одобрение',
          inspections: 'Одити и надзор',
          inspection: 'Одит',
          recommendations: 'Доклад от препоръки',
          auditplans: 'План за надзор',
          staff: 'Персонал',
          staffManagement: 'Ръководен персонал',
          staffExaminers: 'Проверяващи ЛГ',
          addresses: 'Адреси',
          aiportOperator: 'Летищен оператор',
          certAirportOperators:'Лиценз на летищен оператор',
          certGroundServiceOperators:
            'Лиценз на оператор по наземно обслужване или самообслужване',
          groundServiceOperatorsSnoOperational: 'Удостоверение за експлоатационна годност',
          documentOthers: 'Други документи',
          registers: 'Регистри',
          regAirportOperators: 'Издадени лицензи за летищен оператор',
          regGroundServiceOperators:
            'Издадени лицензи за оператор по наземно обслужване или самообслужване'
        },
        search: {
          newOrganization: 'Нова организация',
          search: 'Търси',
          CAO: 'CAO',
          valid: 'Валидност',
          organizationType: 'Тип организация',
          dateValidTo: 'Валидност до',
          dateCAOValidTo: 'САО - дата на валидност'
        },
        viewOrganization: {
          name: 'Наименование',
          CAO: 'CAO',
          uin: 'Булстат',
          organizationType: 'Тип организация',
          edit: 'Редакция'
        },
        newOrganization: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editOrganization: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editCertAirportOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        newCertAirportOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        certAirportOperatorSearch: {
          newCertAirportOperator: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          airport: 'Летище',
          inspector: 'Проверил',
          valid: 'Валиден',
          date: 'Дата на издаване на продължение',
          validToDateExt: 'Дата на изтичане на продължение',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
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
        auditplanSearch: {
          newAuditplan: 'Нов план за одит',
          auditPartRequirement: 'Изискване',
          planYear: 'Година',
          planMonth: 'Месец'
        },
        newAuditplan: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAuditplan: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        staffManagementSearch: {
          newStaffManagement: 'Нов ръководен персонал',
          auditPartRequirement: 'Изискване',
          planYear: 'Година',
          planMonth: 'Месец',
          position: 'Предлагана длъжност',
          person: 'Предложено лице',
          testDate: 'Дата на полагане на писмен тест',
          testScore: 'Оценка от писмен тест',
          number: 'Заявление',
          valid: 'Валиден'
        },
        newStaffManagement: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editStaffManagement: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        staffExaminerSearch: {
          newStaffExaminer: 'Нов проверяващи',
          nomValueId: 'Идентификатор',
          code: 'Код',
          name: 'Наименование',
          valid: 'Валиден',
          person: 'Физическо лице',
          content: {
            stampNumber: '№ на печат',
            organization: 'Организация',
            permitedAW: 'Разрешена проверка на ЛГ',
            permitedCheck: 'Разрешена проверка на лица'
          }
        },
        newStaffExaminer: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editStaffChecker: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        otherSearch: {
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          organizationOtherDocumentType: 'Тип документ',
          organizationOtherDocumentRole: 'Роля',
          valid: 'Действителен',
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
        certGroundServiceOperatorSnoOperationalSearch: {
          newCertGroundServiceOperatorSnoOperational: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          airport: 'Летище',
          inspector: 'Проверил',
          valid: 'Валиден',
          date: 'Дата на издаване на продължение',
          validToDateExt: 'Дата на изтичане на продължение',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
        },
        newCertGroundServiceOperatorsSnoOperational: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editCertGroundServiceOperatorsSnoOperational: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        certGroundServiceOperatorSearch: {
          newCertGroundServiceOperator: 'Нов лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
          organization: 'Организация',
          airport: 'Летище',
          inspector: 'Проверил',
          valid: 'Валиден',
          date: 'Дата на издаване на продължение',
          validToDateExt: 'Дата на изтичане на продължение',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
        },
        newCertGroundServiceOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editCertGroundServiceOperator: {
          save: 'Запис',
          cancel: 'Отказ'
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
        amendmentSearch: {
          newAmendment: 'Ново изменение',
          organizationType: 'Тип одобрение',
          documentNumber: 'Референтен № на описание',
          documentDateIssue: 'Дата на издаване',
          changeNum: 'Изменение',
          back: 'Назад'
        },
        newAmendment: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAmendment: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        approvalSearch: {
          newApproval: 'Ново удостоверение',
          organizationType: 'Тип одобрение',
          documentNumber: ' Номер на одобрението',
          documentNumberAmendment: 'Референтен № на описание',
          documentFirstDateIssue: 'Дата на първо издаване',
          documentDateIssueAmendment: 'Дата на изменение',
          changeNumAmendment: 'Номер на изменение',
          approvalState: 'Състояние'
        },
        newApproval: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        chooseAudits: {
          add: 'Запиши',
          back: 'Назад',
          inspectionFrom: 'Начална дата на изпълнение на одита',
          inspectionTo: 'Крайна дата на изпълнение на одита',
          subject: 'Предмет на одит',
          documentNumber: '№ на документ',
          usedAudits: 'Използвани одити:'
        },
        organizationOtherDirective: {
          title: 'Друг документ',
          documentNumber: 'Док No',
          documentPersonNumber: 'No в списъка (групов документ)',
          documentDateValidFrom: 'Издаден на',
          documentDateValidTo: 'Валидно до',
          documentPublisher: 'Издател',
          notes: 'Бележки',
          organizationOtherDocumentType: 'Тип документ',
          organizationOtherDocumentRole: 'Роля',
          valid: 'Действителен'
        },
        recommendationSearch: {
          newRecommendation: 'Нов доклад',
          recommendationPart: 'Тип',
          formDate: 'Форма за заявен обхват на одобрението от дата',
          formText: 'Форма за заявен обхват на одобрението № на изменението',
          interviewedStaff: 'Интервюиран персонал',
          fromDate: 'Период на надзора от',
          toDate: 'до',
          documentDescription: 'Описание, издание, ревизия Част 3',
          recommendation: 'Препоръки'
        },
        newRecommendation: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRecommendation: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        regAirportOperatorSearch: {
          newRegAirportOperator: 'Ново удостоверение',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          organization: 'Организация',
          address: 'Седалище и адрес на управление на оператора',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
        },
        newRegAirportOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRegAirportOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        regGroundServiceOperatorSearch: {
          newRegGroundServiceOperator: 'Ново удостоверение',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          organization: 'Организация',
          address: 'Седалище и адрес на управление на оператора',
          revokeDate: 'Дата на отнемане',
          revokeCause: 'Причина за отнемане'
        },
        newRegGroundServiceOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        editRegGroundServiceOperator: {
          save: 'Запис',
          cancel: 'Отказ'
        },
        staffManagementDirective: {
          title: 'Ръководен персонал',
          auditPartRequirement: 'Изискване',
          planYear: 'Година',
          planMonth: 'Месец',
          position: 'Предлагана длъжност',
          person: 'Предложено лице',
          testDate: 'Дата на полагане на писмен тест',
          testScore: 'Оценка от писмен тест',
          number: 'Заявление',
          valid: 'Валиден'
        },
        auditplanDirective: {
          title: 'План за одит',
          auditPartRequirement: 'Изискване',
          planYear: 'Година',
          planMonth: 'Месец'
        },
        organizationDataDirective: {
          title: 'Данни за организация',
          name: 'Наименование',
          nameAlt: 'Наименование на поддържащ език',
          code: 'Идентификационен код',
          uin: 'Булстат',
          CAO: 'CAO №',
          dateCAOFirstIssue: 'Първо издаване',
          dateCAOLastIssue: 'Последна ревизия',
          dateCAOValidTo: 'САО - дата на валидност',
          ICAO: 'ICAO №',
          IATA: 'IATA №',
          SITA: 'SITA №',
          organizationType: 'Тип организация',
          organizationKind: 'Вип организация',
          phones: 'Телефони',
          webSite: 'Web сайт',
          notes: 'Бележки',
          valid: 'Валидност',
          dateValidTo: 'Валидност до',
          docRoom: 'Документацията е в стая'
        },
        organizationAddressDirective: {
          title: 'Адрес',
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
        certOperatorDirective: {
          title: 'Лиценз',
          certNumber: '№ на удостоверение',
          issueDate: 'Дата на издаване',
          validToDate: 'Срок на валидност',
          audit: 'Инспекция',
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
          linkedDocumentId: 'Връзка с документ от документите',
          revokeDate: 'Дата на отнемане',
          revokeinspector: 'Инспектор',
          revokeTitle: 'Отнемане',
          documentsTable: {
            includedDocuments: 'Приложени документи',
            inspector: 'Инспектор',
            approvalDate: 'Дата на одобрение',
            linkedDocumentId: 'Връзка с документ от документите',
            noAvailableDocuments: 'Няма налични приложени документи'
          },
          revokeCause: 'Причина за отнемане'
        },
        equipmentDirective: {
          title: 'Съоръжения',
          name: 'Наименование',
          id: 'Инвентарен №',
          count: 'Брой',
          noAvailableEquipments: 'Няма налични съоръжения'
        },
        approvalDirective: {
          title: 'Удостоверение за одобрение',
          organizationType: 'Тип одобрение',
          documentNumber: 'Номер',
          documentDateIssue: 'Дата на издаване',
          approvalState: 'Състояние на одобрението',
          approvalStateDate: 'Дата',
          approvalStateNote: 'Бележки по състоянието'
        },
        amendmentDirective: {
          title: 'Изменение',
          organizationType: 'Тип одобрение',
          documentNumber: 'Референтен № на описание	',
          documentDateIssue: 'Дата на издаване',
          changeNum: '№ на изменение',
          noAvailableLimitations: 'Няма налични данни',
          lims147: {
            title: 'Обхват на одобрение - EASA Форма 11',
            sortOrder: 'Маркер за сортиране',
            lim147limitation: 'Ограничение по част 147',
            lim147limitationText: 'Ограничения - свободен текст'
          },
          lims145: {
            title: 'Обхват на одобрение - EASA Форма 3',
            base: 'Базово',
            lim145limitation: 'Ограничение по част MF/145',
            lim145limitationText: 'Ограничения - свободен текст',
            line: 'Линейно'
          },
          limsMG: {
            title: 'Обхват на одобрение - EASA Форма 14',
            typeAC: 'Тип ВС',
            qualitySystem: 'Организация',
            awapproval: 'Разрешен преглед на летателната годност',
            pfapproval: 'Разрешен Permits to Fly'
          },
          includedDocuments: {
            title: 'Приложени документи към одобрение на организация',
            inspector: 'Инспектор',
            approvalDate: 'Дата на одобрение',
            linkedLim: 'Връзка с Обхват на одобрение',
            linkedDocumentId: 'Връзка с документ от документите на организацията',
            noAvailableDocuments: 'Няма налични документи'
          }
        },
        staffExaminerDirective: {
          title: 'Проверяващ',
          newStaffManagement: 'Нов проверяващи',
          nomValueId: 'Идентификатор',
          code: 'Код',
          name: 'Наименование',
          nameAlt: 'Наименование на поддържащ език',
          alias: 'Псевдоним',
          valid: 'Валиден',
          person: 'Физическо лице',
          content: {
            title: 'Допълнителни данни',
            stampNumber: '№ на печат',
            organization: 'Организация',
            permitedAW: 'Разрешена проверка на ЛГ',
            permitedCheck: 'Разрешена проверка на лица'
          }
        },
        recommendationDirective: {
          title: 'Доклад от препоръки',
          commonData: {
            title: '1. Общи положения',
            auditorsTitle: 'Инспектори по Част 1',
            recommendationPart: 'Тип',
            form: 'Форма за заявен обхват на одобрението',
            formDate: 'от дата',
            formText: '№ на изменението',
            interviewedStaff: 'Интервюиран персонал',
            fromDate: 'Период на надзора от',
            toDate: 'до',
            part1: 'Част 1',
            part2: 'Част 2',
            part3: 'Част 3',
            part4: 'Част 4',
            part5: 'Част 5',
            finished1Date: 'Дата на приключване',
            finished2Date: 'Дата на приключване',
            finished3Date: 'Дата на приключване',
            finished4Date: 'Дата на приключване',
            finished5Date: 'Дата на приключване',
            town: 'Отдел ЛГ гр.',
            documentDescription: 'Описание, издание, ревизия Част 3',
            recommendation: 'Препоръки'
          },
          auditorsReview: {
            title: '2. Одиторски преглед за съответствие',
            auditorsTitle: 'Инспектори по Част 2',
            chooseAudit: 'Избор на одити към доклада от препоръки',
            subject: 'Тема',
            auditResult: 'Констатация',
            disparity: 'Несъответствия',
            auditPart: 'Част',
            chooseAudits: 'Избор на одити към доклада от препоръки'
          },
          accordance: {
            title: '3. Съответсвие с описанието',
            AuditDetailTitle: 'Главни обобщени констатации',
            auditorsTitle: 'Инспектори по Част 3',
            auditPart: 'Част',
            subject: 'Тема',
            auditResult: 'Констатация',
            disparity: 'Несъответствия',
            code: 'Код',
            insertAuditDetails: 'Въведи списъка за обобщени констатации'
          },
          disparities: {
            title: '4. Установени несъответсвия',
            auditorsTitle: 'Инспектори по Част 4',
            disparitiesRecommendationsTitle1: 'Установени несъответствия от одиторски преглед',
            sortOrder: 'Пореден №',
            refNumber: 'Референтен №',
            description: 'Описание на несъответствие',
            subject: 'Тема',
            disparityLevel: 'Ниво',
            removalDate: 'Дата за отстраняване',
            rectifyAction: 'Внесени коригиращи действия',
            closureDate: 'Дата на закриване',
            closureDocument: '№ на документ за закриване',
            auditPart: 'Част'
          },
          recommendations: {
            title: '5. Препоръки',
            auditorsTitle: 'Инспектори по Част 5'
          }
        },
        recomendationAuditorDirective: {
          examiner: 'Одитор',
          sortOrder: 'Пореден №',
          noAvailableExaminers: 'Няма намерени резултати',
          part: 'Част'
        },
        organizationRegisterDirective: {
          titleAirportOperator: 'Регистър за издадени лицензи за летищен оператор',
          titleGroundServiceOperator:
            'Регистър за издадени лицензи за оператор по наземно обслужване или самообслужване',
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
        lin: 'Невалиден ЛИН',
        date: 'Невалидна дата'
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
        'root.applications.edit.case.newFile': 'Нов документ',
        'root.applications.edit.case.addPart': 'Добавяне',
        'root.applications.edit.case.linkPart': 'Свързване',
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
        'root.persons.view.documentApplications': 'Заявления',
        'root.persons.view.documentApplications.new': 'Новo заявление',
        'root.persons.view.documentApplications.edit': 'Редакция на заявление',
        'root.aircrafts': 'ВС',
        'root.aircrafts.new': 'Ново ВС',
        'root.aircrafts.newApex': 'Ново ВС',
        'root.aircrafts.view': 'Данни за ВС',
        'root.aircrafts.view.edit': 'Редакция',
        'root.aircrafts.view.editApex': 'Редакция',
        'root.aircrafts.view.regs': 'Регистрации',
        'root.aircrafts.view.regs.new': 'Нова регистрация',
        'root.aircrafts.view.regs.edit': 'Редакция на регистрация',
        'root.aircrafts.view.regsFM': 'Регистрации',
        'root.aircrafts.view.regsFM.new': 'Нова регистрация',
        'root.aircrafts.view.regsFM.edit': 'Редакция на регистрация',
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
        'root.aircrafts.view.airworthinessesFM': 'Летателни годности',
        'root.aircrafts.view.airworthinessesFM.new': 'Нова годност',
        'root.aircrafts.view.airworthinessesFM.edit': 'Редакция на годност',
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
        'root.aircrafts.view.others.edit': 'Редакция на документ',
        'root.aircrafts.view.owners': 'Свързани лица',
        'root.aircrafts.view.owners.new': 'Ново свързано лице',
        'root.aircrafts.view.owners.edit': 'Редакция на свързано лице',
        'root.aircrafts.view.parts': 'Оборудване',
        'root.aircrafts.view.parts.new': 'Ново оборудване',
        'root.aircrafts.view.parts.edit': 'Редакция на оборудване',
        'root.aircrafts.view.inspections': 'Инспекции',
        'root.aircrafts.view.inspections.new': 'Нова инспекция',
        'root.aircrafts.view.inspections.edit': 'Редакция на инспекция',
        'root.aircrafts.view.occurrences': 'Инциденти',
        'root.aircrafts.view.occurrences.new': 'Нов инцидент',
        'root.aircrafts.view.occurrences.edit': 'Редакция на инцидент',
        'root.aircrafts.view.maintenances': 'Поддръжки',
        'root.aircrafts.view.maintenances.new': 'Новa поддръжка',
        'root.aircrafts.view.maintenances.edit': 'Редакция на поддръжка',
        'root.aircrafts.view.applications': 'Инспекции',
        'root.aircrafts.view.applications.new': 'Нова инспекция',
        'root.aircrafts.view.applications.edit': 'Редакция на инспекция',
        'root.organizations': 'Организации',
        'root.organizations.new': 'Нова организация',
        'root.organizations.view': 'Данни за организация',
        'root.organizations.view.edit': 'Редакция',
        'root.organizations.view.addresses': 'Адреси',
        'root.organizations.view.addresses.new': 'Нов адрес',
        'root.organizations.view.addresses.edit': 'Редакция на адрес',
        'root.organizations.view.certAirportOperators': 'Лицензи на летищен оператор',
        'root.organizations.view.certAirportOperators.new': 'Нов лиценз',
        'root.organizations.view.certAirportOperators.edit': 'Редакция на лиценз',
        'root.organizations.view.auditplans': 'План за надзор',
        'root.organizations.view.auditplans.new': 'Нов план за одит',
        'root.organizations.view.auditplans.edit': 'Редакция на план за одит',
        'root.organizations.view.documentOthers': 'Други документи',
        'root.organizations.view.documentOthers.new': 'Нов документ',
        'root.organizations.view.documentOthers.new.choosePublisher': 'Избор на издател',
        'root.organizations.view.documentOthers.edit': 'Редакция на документ',
        'root.organizations.view.documentOthers.edit.choosePublisher': 'Избор на издател',
        'root.organizations.view.staffManagement': 'Ръководен персонал',
        'root.organizations.view.staffManagement.new': 'Нов ръководен персонал',
        'root.organizations.view.staffManagement.edit': 'Редакция на ръководен персонал',
        'root.organizations.view.certGroundServiceOperators':
          'Лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.certGroundServiceOperators.new':
          'Нов лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.certGroundServiceOperators.edit':
          'Редакция на лиценз на оператор по наземно обслужване или самообслужване',
        'root.organizations.view.groundServiceOperatorsSnoOperational':
          'Удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване',
        'root.organizations.view.groundServiceOperatorsSnoOperational.new':
          'Ново удостоверение',
        'root.organizations.view.groundServiceOperatorsSnoOperational.edit':
          'Редакция на удостоверение',
        'root.organizations.view.recommendations': 'Доклад от препоръки',
        'root.organizations.view.recommendations.new': 'Нов доклад от препоръки',
        'root.organizations.view.recommendations.new.chooseAudits': 'Избор на одити',
        'root.organizations.view.recommendations.new.editDisparity': 'Редакция на несъответствие',
        'root.organizations.view.recommendations.edit': 'Редакция на доклад от препоръки',
        'root.organizations.view.recommendations.edit.chooseAudits': 'Избор на одити',
        'root.organizations.view.recommendations.edit.editDisparity': 'Редакция на несъответствие',
        'root.organizations.view.inspections': 'Одит',
        'root.organizations.view.inspections.new': 'Нов одит',
        'root.organizations.view.inspections.edit': 'Редакция на одит',
        'root.organizations.view.approvals': 'Удостоверение за одобрение',
        'root.organizations.view.approvals.new': 'Ново удостоверение за одобрение',
        'root.organizations.view.approvals.edit': 'Редакция на удостоверение за одобрение',
        'root.organizations.view.amendments': 'Изменения на удостоверение за одобрение',
        'root.organizations.view.amendments.new': 'Ново изменение',
        'root.organizations.view.amendments.edit': 'Редакция на изменение',
        'root.organizations.view.staffExaminers': 'Проверяващи',
        'root.organizations.view.staffExaminers.new': 'Нов проверяващ',
        'root.organizations.view.staffExaminers.edit': 'Редакция на проверяващ',
        'root.organizations.view.regAirportOperators':
          'Регистър за издадени лицензи за летищен оператор',
        'root.organizations.view.regAirportOperators.new': 'Нов лиценз',
        'root.organizations.view.regAirportOperators.edit': 'Редакция на лиценз',
        'root.organizations.view.regGroundServiceOperators':
          'Регистър за издадени лицензи за оператор по наземно обслужване или самообслужване',
        'root.organizations.view.regGroundServiceOperators.new': 'Нов лиценз',
        'root.organizations.view.regGroundServiceOperators.edit': 'Редакция на лиценз',
        'root.airports': 'Летища',
        'root.airports.new': 'Ново летище',
        'root.airports.view': 'Данни за летище',
        'root.airports.view.edit': 'Редакция',
        'root.airports.view.others': 'Други документи',
        'root.airports.view.others.new': 'Нов документ',
        'root.airports.view.others.edit': 'Редакция на документ',
        'root.airports.view.opers': 'Удостоверения за експлоатационна годност',
        'root.airports.view.opers.new': 'Ново удостоверение',
        'root.airports.view.opers.edit': 'Редакция на удостоверение',
        'root.airports.view.applications': 'Заявления',
        'root.airports.view.applications.new': 'Новo заявление',
        'root.airports.view.applications.edit': 'Редакция на заявление',
        'root.airports.view.inspections': 'Инспекции',
        'root.airports.view.inspections.new': 'Нова инспекция',
        'root.airports.view.inspections.edit': 'Редакция на инспекция'
      }
    });
  }]);
}(angular));
