﻿/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      docs: {
        edit: {
          child: 'Подчинен',
          document: 'Документ',
          resolution: 'Резолюция',
          task: 'Задача',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          management: 'Управление',
          docPrepared: 'Отбелязване като изготвен',
          docProcessed: 'Отбелязване като обработен',
          docFinished: 'Отбелязване като приключен',
          docCanceled: 'Отбелязване като анулиран',
          docRegister: 'Регистриране',
          docSignatures: 'Подписване с електронен подпис',
          undoDocSignatures: 'Премахване на ел. подписи',
          sign: 'Подписване',
          discuss: 'Съгласуване',
          approval: 'Одобрение',
          signRequest: 'За подписване',
          discussRequest: 'За съгласуване',
          approvalRequest: 'За одобрение',
          registrationRequest: 'За регистрация',
          reverseDocPrepared: 'Връщане в статус чернова',
          reverseDocProcessed: 'Връщане в статус изготвен',
          reverseDocFinished: 'Връщане в статус обработен',
          reverseDocCanceled: 'Връщане в статус обработен',
          stagesName: 'Етапи',
          endElectronicStage: 'Приключване',
          nextElectronicStage: 'Следващ',
          techEditElectronicStage: 'Техн. редакция',
          reverseElectronicStage: 'Сторниране',
          markRead: 'Маркирай като прочетено',
          markUnRead: 'Маркирай като непрочетено',
          regDate: 'Дата и час',
          regUri: 'Номер',
          docStatusName: 'Статус',
          docDirectionName: 'Тип',
          addressing: {
            content: 'Прикачени файлове',
            text: 'Текст',
            classification: 'Код за достъп и класификация',
            accessCode: 'Код за достъп',
            permissions: 'Допълнителни права',
            assignment: 'Възлагане',
            readers: 'Читатели',
            editors: 'Редактори',
            registrators: 'Регистратори',
            assignmentType: 'Вид възлагане',
            assignmentDate: 'Дата на възлагане',
            assignmentDeadline: 'Срок до',
            controlling: 'Контролиращ',
            inCharge: 'Отговорник',
            corrRegNumber: 'Коресп. №',
            corrRegDate: 'Дата',
            docSourceType: 'Получено чрез',
            docDestinationType: 'Изпратено чрез',
            docCorrespondents: 'Кореспонденти',
            cCopy: 'Копие до',
            importedBy: 'Въвел',
            madeBy: 'Изготвил',
            docUnitsFrom: 'От',
            docUnitsTo: 'До',
            selectCorr: {
              displayName: 'Наименование',
              email: 'Имейл',
              correspondentType: 'Тип',
              search: 'Търси',
              cancel: 'Назад',
              select: 'Избор'
            },
            selectUnit: {
              name: 'Име',
              search: 'Търси',
              cancel: 'Назад',
              select: 'Избор'
            }
          },
          content: {
            docBody: 'Текст',
            privateDocFiles: 'Вътрешни файлове',
            publicDocFiles: 'Публични файлове'
          },
          workflows: {
            eventDate: 'Дата',
            docWorkflowActionName: 'Действие',
            yesNo: 'Отговор',
            principalUnitName: 'От',
            toUnitName: 'До',
            note: 'Забележка',
            'delete': 'изтрий'
          },
          stages: {
            startingDate: 'Нач. дата',
            esStageName: 'Етап',
            esStageExecutors: 'Изпълнител',
            expectedEndingDate: 'Очаквана дата на прикл.',
            endingDate: 'Дата на приключване',
            isCurrentStage: 'Текущ'
          },
          'case': {
            regDate: 'Дата',
            regNumber: 'Рег.№',
            statusName: 'Статус',
            description: 'Описание',
            viewDoc: 'преглед',
            viewApplication: 'преглед',
            doc: 'Документ',
            application: 'Заявление'
          },
          classifications: {
            name: 'Класификационна схема',
            date: 'Дата',
            'delete': 'изтрий',
            add: 'добави'
          }

        },
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
          correspondentType: 'Тип',
          onlyActive: 'Само активни',
          onlyUnactive: 'Само неактивни',
          'new': 'Нов кореспондент',
          search: 'Търси'
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
        'root.docs.edit.data.view': 'Преглед',
        'root.docs.edit.data.view.selectCorr': 'Избор на кореспондент',
        'root.docs.edit.data.view.selectUnit': 'Избор на служител',
        'root.docs.edit.data.workflows': 'Управление',
        'root.docs.edit.data.stages': 'Етапи',
        'root.docs.edit.data.case': 'Преписка',
        'root.corrs': 'Кореспонденти',
        'root.corrs.new': 'Нов кореспондент',
        'root.corrs.edit': 'Редакция'
      }
    });
  }]);
}(angular));
