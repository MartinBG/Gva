/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      docs: {
        search: {
          fromDate: 'От дата',
          toDate: 'До дата',
          docName: 'Относно',
          docTypeId: 'Вид на документа',
          docStatusId: 'Статус на документа',
          corrs: 'Кореспонденти',
          units: 'Отнесено към',
          view: 'Преглед',
          choose: 'Избор',
          newDoc: 'Нов документ',
          regDate: 'Дата',
          regUri: 'Рег.№',
          docSubject: 'Относно',
          docDirectionName: '',
          docStatusName: 'Статус',
          correspondentName: 'Кореспондент'
        },
        newDoc: {
          caseRegUri: 'Към преписка',
          docTypeGroupId: 'Група',
          docTypeId: 'Вид',
          docSubject: 'Относно',
          docCorrespondent: 'Кореспондент',
          numberOfDocs: 'Брой документи'
        }
      },
      units: {
        name: 'Наименование',
        search: 'Търси',
        edit: 'Редакция',
        back: 'Назад',
        select: 'Избор'
      },
      corrs: {
        search: {
          displayName: 'Наименование',
          email: 'Имейл',
          activity: 'Активност',
          onlyActive: 'Само активни',
          onlyUnactive: 'Само неактивни',
          'new': 'Нов кореспондент',
          search: 'Търси',
          back: 'Назад',
          select: 'Избор'
        },
        edit: {
          correspondentTitle: 'Кореспондент',
          email: 'Имейл',
          correspondentType: 'Тип кореспондент',
          correspondentGroup: 'Кореспондентска група',
          bgCitizenFirstName: 'Първо име',
          bgCitizenLastName: 'Фамилия',
          bgCitizenUIN: 'ЕГН',
          foreignerFirstName: 'Първо име',
          foreignerLastName: 'Фамилия',
          foreignerCountry: 'Държава',
          foreignerSettlement: 'Населено място',
          foreignerBirthDate: 'Дата на раждане',
          legalEntityName: 'Наименование',
          legalEntityBulstat: 'БУЛСТАТ',
          fLegalEntityName: 'Наименование',
          fLegalEntityCountry: 'Държава',
          fLegalEntityRegisterName: 'Рег. наименование',
          fLegalEntityRegisterNumber: 'Рег. номер',
          fLegalEntityOtherData: 'Доп. информация',
          contactDistrictId: 'Област',
          contactMunicipalityId: 'Община',
          contactSettlementId: 'Населено място',
          contactPostCode: 'ПК',
          contactAddress: 'Адрес',
          contactPostOfficeBox: 'Пощенска кутия',
          contactPhone: 'Телефон',
          contactFax: 'Факс',
          contactPersonsTitle: 'Лица за контакти',
          contactName: 'Наименование',
          contactUin: 'Идентификационен номер',
          contactNote: 'Позиция',
          'delete': 'изтрий',
          add: 'добави',
          save: 'Запис',
          cancel: 'Отказ'
        }
      },
      states: {
        'root.docs': 'Документи',
        'root.docs.new': 'Нов документ',
        'root.docs.edit': 'Редакция',
        'root.docs.edit.addressing': 'Адресати',
        'root.docs.edit.chooseCorr': 'Избор кореспондент',
        'root.docs.edit.chooseUnit': 'Избор служител',
        'root.docs.edit.content': 'Прикачени файлове',
        'root.docs.edit.workflows': 'Управление',
        'root.docs.edit.stages': 'Етапи',
        'root.docs.edit.case': 'Преписка',
        'root.docs.edit.classifications': 'Класификация',
        'root.corrs': 'Кореспонденти',
        'root.corrs.new': 'Нов кореспондент',
        'root.corrs.edit': 'Редакция'
      }
    });
  }]);
}(angular));
