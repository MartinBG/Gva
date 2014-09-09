﻿/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems').config(['l10nProvider', function (l10n) {
    l10n.add('bg-bg', {
      common: {
        chooseCorr: {
          title: 'Избор на кореспондент',
          displayName: 'Наименование',
          email: 'Имейл',
          correspondentType: 'Тип',
          search: 'Търси',
          cancel: 'Отказ',
          select: 'Избор',
          'new': 'Нов'
        },
        newCorr: {
          title: 'Нов кореспондент',
          cancel: 'Отказ',
          save: 'Запис'
        },
        chooseUnit: {
          title: 'Избор на служител',
          name: 'Име',
          search: 'Търси',
          cancel: 'Отказ',
          select: 'Избор'
        },
        sendTransferDoc: {
          title: 'Изпращане на документа за препращане по компетентност',
          receiverServiceProvider: 'Изпрати към',
          cancel: 'Отказ',
          send: 'Изпрати'
        }
      },
      docs: {
        edit: {
          child: 'Подчинен',
          document: 'Документ',
          electronicDocument: 'Електронен документ',
          resolution: 'Резолюция',
          resolutionParentOnly: 'Резолюция върху документ',
          remark: 'Забележка',
          task: 'Задача',
          taskParentOnly: 'Задача върху документ',
          edit: 'Редакция',
          save: 'Запис',
          cancel: 'Отказ',
          management: 'Управление',
          caseManagement: 'Преписка',
          casePart: 'Част на преписка',
          docType: 'Техн. редакция на документ',
          docPrepared: 'Отбелязване като изготвен',
          docProcessed: 'Отбелязване като обработен',
          docFinished: 'Отбелязване като приключен',
          docCanceled: 'Отбелязване като анулиран',
          docRegister: 'Регистриране',
          docSignatures: 'Подписване с електронен подпис',
          docSend: 'Изпращане',
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
          docSubject: 'Относно',
          regDate: 'Дата и час',
          regUri: 'Номер',
          docStatusName: 'Статус',
          docDirectionName: 'Тип',
          view: {
            docFile: {
              kind: 'Вид файл',
              name: 'Наименование',
              type: 'Тип файл',
              attachedFile: 'Прикачен файл'
            },
            content: 'Прикачени файлове',
            editableFile: 'Данни',
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
            docUnitsTo: 'До'
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
            author: 'Въвел',
            'delete': 'изтрий',
            request: {
              toUnit: 'Към служител',
              toUnitSign: 'Към служител за ПОДПИСВАНЕ',
              toUnitDiscuss: 'Към служител за СЪГЛАСУВАНЕ',
              toUnitApproval: 'Към служител за ОДОБРЕНИЕ',
              toUnitRegistration: 'Към служител за РЕГИСТРАЦИЯ',
              fromUser: 'От',
              note: 'Забележка',
              save: 'Запис',
              cancel: 'Отказ'
            },
            confirm: {
              author: 'Въвел',
              fromUser: 'От служител',
              note: 'Забележка',
              sign: 'ПОДПИСВАМ',
              approve: 'ОДОБРЯВАМ',
              discuss: 'СЪГЛАСУВАМ',
              save: 'Запис',
              cancel: 'Отказ'
            }
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
            receiptOrder: '#',
            regDate: 'Дата',
            regNumber: 'Рег.№',
            statusName: 'Статус',
            description: 'Описание',
            viewDoc: 'преглед',
            viewApplication: 'Преглед на заявление',
            linkApplication: 'Свържи със заявление',
            doc: 'Документ',
            application: 'Заявление',
            casePart: {
              partOf: 'Част от преписка',
              save: 'Запис',
              cancel: 'Отказ'
            },
            caseFinish: {
              notFinishedMsg: 'Следните документи не са приключени в преписката:',
              regDate: 'Дата',
              regNumber: 'Рег.№',
              statusName: 'Статус',
              description: 'Описание',
              finish: 'Приключи',
              save: 'Приключване',
              cancel: 'Отказ'
            },
            changeDocParent: {
              title: 'Премести в преписка',
              newCase: 'Създай нова преписка',
              cancel: 'Отказ',
              select: 'Избор'
            },
            docType: {
              registerIndex: 'Регистър на документа',
              unregisterDoc: 'Дерегистриране на документа?',
              receiptOrder: 'Пореден номер',
              docTypeGroup: 'Група',
              docType: 'Вид',
              docDirection: 'Част от преписка',
              save: 'Запис',
              cancel: 'Отказ',
              from: 'От',
              to: 'До',
              cCopy: 'Копие до',
              importedBy: 'Въвел',
              madeBy: 'Изготвил',
              inCharge: 'Отговорник',
              controlling: 'Контролиращ',
              roleReaders: 'Читатели',
              editors: 'Редактори',
              roleRegistrators: 'Регистратори'
            },
            manualRegister: {
              manualNumber: 'Номер на документ',
              save: 'Запис',
              cancel: 'Отказ'
            }
          },
          classifications: {
            name: 'Класификационна схема',
            date: 'Дата',
            'delete': 'изтрий',
            add: 'добави',
            edit: 'Техн. редакция на кл. схема',
            isInherited: 'Наследяемо',
            isActive: 'Активно',
            save: 'Запис',
            cancel: 'Отказ'
          },
          sendEmail: {
            emailTo: 'До',
            emailBcc: 'Копие до',
            publicFiles: 'Прикачени файлове',
            noFiles: 'Няма прикачени файлове',
            subject: 'Относно',
            body: 'Съдържание',
            send: 'Изпрати',
            cancel: 'Отказ'
          }
        },
        nextStage: {
          stage: 'Етап',
          startingDate: 'Начална дата',
          executors: 'Изпълнители',
          expectedEndingDate: 'Очаквана дата прикл.',
          endingDate: 'Дата на приключване',
          save: 'Запис',
          cancel: 'Отказ'
        },
        search: {
          current: 'Текущи преписки',
          finished: 'Приключени преписки',
          manage: 'За управление',
          control: 'За контрол и изпълнение',
          draft: 'Чернови',
          unfinished: 'Неприключени',
          all: 'Всички',
          portal: 'От портал',
          allControl: 'Всички (Контролни)',
          search: 'Търсене',
          fromDate: 'От дата',
          toDate: 'До дата',
          docName: 'Относно',
          docType: 'Вид на документа',
          docStatus: 'Статус на документа',
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
          correspondentName: 'Кореспондент',
          kase: 'Преписка'
        },
        newDoc: {
          kontinue: 'Продължи',
          registerAndShow: 'Регистрирай и продължи',
          register: 'Регистрирай',
          caseRegUri: 'Към преписка',
          cancel: 'Отказ',
          docTypeGroupId: 'Група',
          docTypeId: 'Вид',
          docSubject: 'Относно',
          docCorrespondent: 'Кореспондент',
          docNumbers: 'Брой документи'
        },
        caseSelect: {
          fromDate: 'От дата',
          toDate: 'До дата',
          docName: 'Относно',
          docType: 'Вид на документа',
          docStatus: 'Статус на документа',
          corrs: 'Кореспонденти',
          units: 'Отнесено към',
          select: 'Избор',
          regDate: 'Дата',
          regUri: 'Рег.№',
          docSubject: 'Относно',
          docDirectionName: '',
          docStatusName: 'Статус',
          correspondentName: 'Кореспондент',
          search: 'Търси',
          cancel: 'Отказ'
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
          contactDataForm: 'Лични данни',
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
          cancel: 'Отказ',
          correspondentContact: {
            add: 'Добавяне',
            save: 'Запис',
            cancel: 'Отказ',
            edit: 'Редакция',
            'delete': 'Изтриване'
          }
        }
      },
      removingIrregularity: {
        serviceProviderName: 'От',
        firstName: 'Име',
        secondName: 'Презиме',
        lastName: 'Фамилия',
        applicant: 'До',
        egn: 'ЕГН',
        email: 'Електронна поща',
        registerIndex: 'Рег. индекс',
        sequenceNumber: 'Пореден номер',
        date: 'Дата',
        caseUri: '№ на преписка',
        applicationUri: '№ на заявление',
        irregularityDocUri: '№ на документ',
        administrativeBodyName: 'Aдминистративен орган',
        instructionsHeader: 'Инструкции за остраняване на нередностите',
        employ: 'Длъжностно лице',
        deadlinePeriod: 'Срок за остраняване на нередностите',
        irregularities: 'Списък с нередности за остраняване',
        irregularityType: 'Тип нередност'
      },
      receiptAcknowledge: {
        serviceProviderName: 'От',
        firstName: 'Име',
        secondName: 'Презиме',
        lastName: 'Фамилия',
        egn: 'ЕГН',
        email: 'Електронна поща',
        applicant: 'До',
        registeredBy: 'Регистратор на документа',
        documentUri: '№ на документ',
        registerIndex: 'Рег. индекс',
        sequenceNumber: 'Пореден номер',
        date: 'Дата',
        caseInfo: 'Данни за преписка',
        caseUri: '№ на преписка',
        caseAccessCode: 'Код за достъп'
      },
      receiptNotAcknowledge: {
        serviceProviderName: 'От',
        firstName: 'Име',
        secondName: 'Презиме',
        lastName: 'Фамилия',
        egn: 'ЕГН',
        email: 'Електронна поща',
        applicant: 'До',
        documentUri: '№ на документ',
        registerIndex: 'Рег. индекс',
        sequenceNumber: 'Пореден номер',
        date: 'Дата',
        discrepancies: 'Списък с несъответствия',
        discrepancyType: 'Несъответствие'
      },
      competenceTransfer: {
        senderServiceProviderName: 'От',
        receiverServiceProviderName: 'До',
        caseUri: '№ на преписка',
        documentUri: '№ на документ',
        registerIndex: 'Рег. индекс',
        sequenceNumber: 'Пореден номер',
        date: 'Дата',
        applicant: 'Заявител',
        firstName: 'Име',
        secondName: 'Презиме',
        lastName: 'Фамилия',
        egn: 'ЕГН',
        documents: 'Документи',
        docType: 'Вид на документ',
        download: 'Свали',
        view: 'Преглед',
        sendDoc: 'Изпрати документа'
      },
      portalModal: {
        eApplication: {
          title: 'Електронно заявление',
          cancel: 'Отказ'
        }
      },
      noms: {
        units: {
          name: 'Служители',
          form: {
            unit: 'Родител',
            name: 'Име',
            unitType: 'Тип',
            isActive: 'Активен'
          },
          edit: {
            unitTitle: 'Служител',
            save: 'Запис',
            cancel: 'Отказ',
            deleteUnit: 'Изтриване',
            confirmDelete: 'Сигурни ли сте, че искате да изтриете данните?'
          },
          'new': {
            unitTitle: 'Служител',
            save: 'Запис',
            cancel: 'Отказ'
          },
          search: {
            unitTypeId: 'Тип',
            search: 'Търси',
            'new': 'Нов служител',
            parentName: 'Родител',
            name: 'Име',
            unitTypeName: 'Тип',
            isActive: 'Активен'
          }
        }
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
      states: {
        'root.docs': 'Документи',
        'root.docs.new': 'Нов документ',
        'root.docs.new.caseSelect': 'Избор на преписка',
        'root.docs.edit.view': 'Преглед',
        'root.docs.edit.workflows': 'Управление',
        'root.docs.edit.workflows.signRequest': 'Искане за подпис',
        'root.docs.edit.workflows.discussRequest': 'Искане за съгласуване',
        'root.docs.edit.workflows.approvalRequest': 'Искане за одобрение',
        'root.docs.edit.workflows.registrationRequest': 'Искане за регистрация',
        'root.docs.edit.workflows.signConfirm': 'Подписване',
        'root.docs.edit.workflows.discussConfirm': 'Съгласуване',
        'root.docs.edit.workflows.approvalConfirm': 'Одобрение',
        'root.docs.edit.stages': 'Етапи',
        'root.docs.edit.stages.next': 'Следващ етап',
        'root.docs.edit.stages.edit': 'Редакция на етап',
        'root.docs.edit.stages.end': 'Приключване на етап',
        'root.docs.edit.case': 'Преписка',
        'root.docs.edit.case.linkApp': 'Свържи със заявление',
        'root.docs.edit.case.casePart': 'Смяна на част на преписка',
        'root.docs.edit.case.docType': 'Техническа редакция на документ',
        'root.docs.edit.case.caseFinish': 'Приключване на преписка',
        'root.corrs': 'Кореспонденти',
        'root.corrs.new': 'Нов кореспондент',
        'root.corrs.edit': 'Редакция',
        'root.noms': 'Номенклатури',
        'root.noms.units': 'Служители'
      }
    });
  }]);
}(angular));
