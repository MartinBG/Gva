/*global angular*/
(function (angular) {
  'use strict';
  angular.module('mosv').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      admissions: {
        admissionDirective: {
          title: 'Партида в регистър за Предоставяне на достъп до обществена информация',
          incomingLot: 'Партида',
          incomingNumber: 'Вх.номер',
          incomingDate: 'Дата',
          applicantType: 'Вид заявител',
          applicant: 'Заявител',
          provideType: 'Начин на предоставяне',
          clarification: 'Уточнение',
          informationType: 'Вид на информацията',
          theme: 'Тема',
          about: 'Относно',
          fee: 'Такса',
          paymentType: 'Начин на плащане',
          paidFee: 'Таксата е платена',
          status: 'Статус',
          acceptanceTitle: 'Решение:',
          acceptanceResolutionNumber: 'Вх.номер',
          acceptanceResolutionDate: 'Дата',
          denialTitle: 'Без разглеждане:',
          //denialResolutionNumber: 'Изх.номер',
          //denialResolutionDate: 'Дата',
          reason: 'Причина',
          motives: 'Мотиви',
          appeal: 'Обжалване',
          appealDate: 'Дата на обжалване',
          appealResult: 'Резултат от обжалване'
        },
        search: {
          search: 'Търси',
          newAdmission: 'Нов достъп',
          incomingLot: 'Партида',
          incomingNumber: 'Вх.номер',
          incomingDate: 'Дата',
          applicantType: 'Вид заявител',
          applicant: 'Заявител'
        },
        newAdmission: {
          title: 'Нов достъп',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editAdmission: {
          title: 'Преглед на достъп',
          save: 'Запис',
          cancel: 'Отказ',
          edit: 'Редакция'
        }
      },
      suggestions: {
        suggestionDirective: {
          lot: 'Партида',
          number: 'Вх.номер',
          date: 'Дата',
          submitType: 'Начин на подаване',
          applicant: 'Заявител',
          institution: 'Отговорна институция',
          character: 'Естество на предложението',
          actions: 'Предприети действия',
          status: 'Статус'
        },
        newSuggestion: {
          title: 'Ново предложение',
          save: 'Запис',
          cancel: 'Отказ'
        },
        editSuggestion: {
          title: 'Преглед на предложение',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        },
        search: {
          newSuggestion: 'Ново предложение',
          search: 'Търси',
          incomingLot: 'Партида',
          incomingNumber: 'Вх. номер',
          applicant: 'Заявител',
          incomingDateFrom: 'Дата от',
          incomingDateТо: 'Дата до',
          incomingDate: 'Дата'
        }
      },
      signals: {
        signalDirective: {
          title: 'Партида в регистър за Сигнали',
          incomingLot: 'Партида',
          number: 'Вх.номер',
          date: 'Дата',
          submitType: 'Начин на подаване',
          applicant: 'Заявител',
          institution: 'Отговорна институция',
          violation: 'Извършено нарушение',
          violationPlace: 'Място на извършване на нарушението',
          affectedParts: 'Кой/и компонент/и или фактор/и на околната среда са засегнати',
          checkTime: 'Препоръчително време за извършване на проверка',
          damages: 'Какви са нанесените вреди от нарушението',
          precautions: 'Вземани ли са други мерки до момента',
          actions: 'Предприети действия',
          status: 'Статус'
        },
        search: {
          incomingLot: 'Партида',
          incomingNumber: 'Вх.номер',
          incomingDate: 'Дата',
          applicant: 'Заявител',
          violation: 'Извършено нарушение',
          institution: 'Институция',
          search: 'Търси',
          newSignal: 'Нов сигнал'
        },
        'new': {
          title: 'Нов сигнал',
          save: 'Запис',
          cancel: 'Отказ'
        },
        edit: {
          title: 'Преглед на сигнал',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      decision: {
        legalBasisIssuanceAdministrativeAct:
          'На основание',
        legalBasisIssuanceAdministrativeActDescription:
          '(Правно основание за издаване на индивидуален административен акт)',
        explanation:
          'след разглеждане на заявление за достъп до обществена информация с № , намирам, '+
          'че са налице основания за предоставяне на исканата обществена информация и поради това',
        degreeAccessRequestedPublicInformation:
          'Предоставям',
        degreeAccessRequestedPublicInformationDescription:
          '(Степен на осигурения достъп до исканата обществена информация)',
        descriptionProvidedPublicInformation:
          'Исканата информация от посоченото заявление',
        descriptionProvidedPublicInformationDescription:
          '(Описание на предоставяната обществена информация)',
        formProvidingAccessPublicInformation:
          'Да се предостави в пълен обем, според искането на заявителя посредством',
        formProvidingAccessPublicInformationDescription:
          '(Форма за предоставяне на достъп до обществена информация)',
        placePublicInformationAccessGiven:
          'Разрешената информация може да бъде получена (в/от/изпратена)',
        placePublicInformationAccessGivenDescription:
          '(Място, където ще бъде предоставен достъп до исканата обществена информация)',
        periodAccessPublicInformation:
          'Информацията да се предостави за срок',
        periodAccessPublicInformationDescription:
          '(Срок, в който е осигурен достъп до исканата обществена информация)',
        informationPaymentCostsProvidingAccess:
          'Информация за заплащане на разходи по предоставянето на достъп до обществена информация',
        otherBodiesOrganizations:
          'Други органи, организации или лица, които разполагат с по-пълна '+
          'информация относно естеството на поисканата обществена информация',
        personType:
          'Длъжностно лице',
        firstName:
          'Собствено име',
        middleName:
          'Бащино име',
        lastName:
          'Фамилно име',
        otherName:
          'Други имена',
        electronicDocumentAuthorQuality:
          'Качество, в което лицето представлява административния орган'
      },
      states: {
        'root.signals': 'Сигнали',
        'root.signals.new': 'Нов сигнал',
        'root.signals.edit': 'Редакция на сигнал',
        'root.admissions': 'Достъпи',
        'root.admissions.new': 'Нов достъп',
        'root.admissions.edit': 'Редакция на достъп',
        'root.suggestions': 'Предложения',
        'root.suggestions.new': 'Ново предложение',
        'root.suggestions.edit': 'Редакция на предложение'
      }
    });
  }]);
})(angular);
